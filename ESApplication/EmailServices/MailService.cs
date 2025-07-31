using ESApplication.AggregateModels;
using ESApplication.Models;
using ESApplication.Responses;
using ESInfrastructure.Database;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;


namespace ESApplication.EmailServices
{
    public class MailService : IMailService
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings, ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _mailSettings = mailSettings.Value;
        }

        private void UpdateEmailDataManagement(Int64 id)
        {
            using (var _conn = this._sqlConnectionFactory.GetOpenConnection())
            {
                using (var tr = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "uspUpdateEmailDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@id", SqlDbType.BigInt).Value = id; 
                            command.ExecuteNonQuery();
                        }
                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                    }
                }
            }
        }

        public async Task SendEmailAsync()
        {
            MailRequest mailRequest;
            var response = new List<EmailConfigDto>();
            using (var _conn = this._sqlConnectionFactory.GetOpenConnection())
            {
                using (var tr = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "uspGetEmailConfig";
                            command.Transaction = tr;
                            DataTable dt = new DataTable();

                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                            {
                                dataAdapter.SelectCommand = command;
                                dataAdapter.Fill(dt);
                            }

                            foreach (DataRow row in dt.Rows)
                            {
                                response.Add(CreateFromSqlDataReader(row));
                            }

                            for (int i = 0; i < response.Count; i++)
                            {
                                mailRequest = new MailRequest();
                                mailRequest.Subject = response[0].subject;
                                mailRequest.Body = response[0].body;
                                mailRequest.ToEmail = response[0].email;
                                SendEmailAsync(mailRequest);
                            }
                        }
                        tr.Commit();
                        for (int i = 0; i < response.Count; i++)
                            UpdateEmailDataManagement(response[i].id);
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                    }
                }
            }
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, false);
                //smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

            }
        }
        public EmailConfigDto CreateFromSqlDataReader(DataRow row)
        {
            var emailConfigDto = new EmailConfigDto()
            {
                id = Convert.ToInt64(row["id"]),
                sequence = Convert.ToInt32(row["sequence"]),
                subject = row["subject"].ToString(),
                body = row["body"].ToString(),
                email = row["email"].ToString(),
            };
            return emailConfigDto;
        }
    }
}
