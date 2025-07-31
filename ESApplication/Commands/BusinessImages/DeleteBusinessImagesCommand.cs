using ESApplication.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESApplication.Commands.BusinessImages
{
    public class DeleteBusinessImagesCommand : IRequest<Response<string>>
    {
        public int Action { get; set; }
        public string UserId { get; set; }
        public string Id { get; set; }
        public string? ImagePath { get; set; }
        public Int64 Isactive { get; set; }

    }

}
