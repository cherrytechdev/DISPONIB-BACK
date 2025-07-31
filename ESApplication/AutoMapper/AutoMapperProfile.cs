using AutoMapper;
using ESApplication.Commands.Category;
using ESApplication.Commands.SiteData;
using ESApplication.Commands.UserDetails;
using ESApplication.Commands.Promotion;
using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.PromotionAggregate;
using ESDomain.AggregateModels.SiteDataAggregate;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.AggregateModels.BusinessAggregate;
using ESApplication.Commands.Business;
using ESDomain.AggregateModels.BusinessHoursAggregate;
using ESApplication.Commands.BusinessHours;
using ESApplication.Commands.BusinessImages;
using ESDomain.AggregateModels.BusinessImageAggregate;
using ESDomain.AggregateModels.CommonAggregate;
using ESApplication.Commands.CommonData;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ESApplication.Commands.TokenDetails;
using ESDomain.AggregateModels.TokenDetailsAggregate;

namespace ESApplication.AutoMapper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<CreateUserDetailsCommand, UserDetail>()
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username))
                //.ForMember(dest => dest.roleid, opt => opt.MapFrom(src => src.roleid))
                .ForMember(dest => dest.firstname, opt => opt.MapFrom(src => src.firstname))
                .ForMember(dest => dest.lastname, opt => opt.MapFrom(src => src.lastname))
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => Utilities.Encrypt(src.password)))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.mobile, opt => opt.MapFrom(src => src.mobile))
                .ForMember(dest => dest.businessid, opt => opt.MapFrom(src => src.businessid))
                .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.comment))
                .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.isquiz, opt => opt.MapFrom(src => src.isquiz));

            CreateMap<UpdateUserDetailsCommand, UserDetail>()
            .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username))
            .ForMember(dest => dest.firstname, opt => opt.MapFrom(src => src.firstname))
            .ForMember(dest => dest.lastname, opt => opt.MapFrom(src => src.lastname))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
            .ForMember(dest => dest.mobile, opt => opt.MapFrom(src => src.mobile))
            .ForMember(dest => dest.businessid, opt => opt.MapFrom(src => src.businessid))
            .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.comment))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
            .ForMember(dest => dest.isquiz, opt => opt.MapFrom(src => src.isquiz));

            CreateMap<DeleteUserDetailsCommand, UserDetail>()
              .ForMember(dest => dest.action, opt => opt.MapFrom(src => src.action))
              .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
              .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
              .ForMember(dest => dest.isactive, opt => opt.MapFrom(src => src.isactive));


            CreateMap<CreateSiteDataCommand, SiteDataDetails>()
            .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
            .ForMember(dest => dest.filename, opt => opt.MapFrom(src => src.filename))
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
            .ForMember(dest => dest.base64text, opt => opt.MapFrom(src => src.base64text))
            .ForMember(dest => dest.filepath, opt => opt.MapFrom(src => src.filePath));




            CreateMap<UpdateSiteDataCommand, SiteDataDetails>()
            .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
            .ForMember(dest => dest.filename, opt => opt.MapFrom(src => src.filename))
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
            .ForMember(dest => dest.base64text, opt => opt.MapFrom(src => src.base64text));

            CreateMap<DeleteSiteDataCommand, SiteDataDetails>()
            .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id));

            CreateMap<CreateCategoryCommand, CategoryDetails>()
            .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
            .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.code))
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
            .ForMember(dest => dest.categorycode, opt => opt.MapFrom(src => src.categorycode));

            CreateMap<UpdateCategoryCommand, CategoryDetails>()
            .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.code))
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
            .ForMember(dest => dest.categorycode, opt => opt.MapFrom(src => src.categorycode));

            CreateMap<DeleteCategoryCommand, CategoryDetails>()
           .ForMember(dest => dest.action, opt => opt.MapFrom(src => src.action))
           .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
           .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
           .ForMember(dest => dest.isactive, opt => opt.MapFrom(src => src.isactive))
           .ForMember(dest => dest.categorycode, opt => opt.MapFrom(src => src.categorycode));

            CreateMap<CreatePromotionCommand, PromotionDetails>()
           .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
           .ForMember(dest => dest.couponcode, opt => opt.MapFrom(src => src.couponcode))
           .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
           .ForMember(dest => dest.startdate, opt => opt.MapFrom(src => src.startdate))
           .ForMember(dest => dest.enddate, opt => opt.MapFrom(src => src.enddate))
            .ForMember(dest => dest.discount, opt => opt.MapFrom(src => src.discount));

            CreateMap<UpdatePromotionCommand, PromotionDetails>()
            .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
           .ForMember(dest => dest.couponcode, opt => opt.MapFrom(src => src.couponcode))
           .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
           .ForMember(dest => dest.startdate, opt => opt.MapFrom(src => src.startdate))
           .ForMember(dest => dest.enddate, opt => opt.MapFrom(src => src.enddate))
            .ForMember(dest => dest.discount, opt => opt.MapFrom(src => src.discount));

            CreateMap<DeletePromotionCommand, PromotionDetails>()
           .ForMember(dest => dest.action, opt => opt.MapFrom(src => src.action))
           .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
           .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
           .ForMember(dest => dest.isactive, opt => opt.MapFrom(src => src.isactive));

            CreateMap<CreateBusinessCommand, BusinessDetails>()
           .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
           .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.title))
           .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
           .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.address))
           .ForMember(dest => dest.categorycode, opt => opt.MapFrom(src => src.categorycode))
           .ForMember(dest => dest.subcategorycode, opt => opt.MapFrom(src => src.subcategorycode))
           .ForMember(dest => dest.mobile, opt => opt.MapFrom(src => src.mobile))
           .ForMember(dest => dest.whatsapp, opt => opt.MapFrom(src => src.whatsapp))
           .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
           .ForMember(dest => dest.alternativeemail, opt => opt.MapFrom(src => src.alternativeemail))
           .ForMember(dest => dest.website, opt => opt.MapFrom(src => src.website))
           .ForMember(dest => dest.instagram, opt => opt.MapFrom(src => src.instagram))
           .ForMember(dest => dest.facebook, opt => opt.MapFrom(src => src.facebook))
           .ForMember(dest => dest.linkedin, opt => opt.MapFrom(src => src.linkedin))
           .ForMember(dest => dest.couponcode, opt => opt.MapFrom(src => src.couponcode));

            CreateMap<UpdateBusinessCommand, BusinessDetails>()
          .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
          .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
          .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.title))
          .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
          .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.address))
          .ForMember(dest => dest.categorycode, opt => opt.MapFrom(src => src.categorycode))
          .ForMember(dest => dest.subcategorycode, opt => opt.MapFrom(src => src.subcategorycode))
          .ForMember(dest => dest.mobile, opt => opt.MapFrom(src => src.mobile))
          .ForMember(dest => dest.whatsapp, opt => opt.MapFrom(src => src.whatsapp))
          .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
          .ForMember(dest => dest.alternativeemail, opt => opt.MapFrom(src => src.alternativeemail))
          .ForMember(dest => dest.website, opt => opt.MapFrom(src => src.website))
          .ForMember(dest => dest.instagram, opt => opt.MapFrom(src => src.instagram))
          .ForMember(dest => dest.facebook, opt => opt.MapFrom(src => src.facebook))
          .ForMember(dest => dest.linkedin, opt => opt.MapFrom(src => src.linkedin))
          .ForMember(dest => dest.couponcode, opt => opt.MapFrom(src => src.couponcode));

            CreateMap<DeleteBusinessCommand, BusinessDetails>()
           .ForMember(dest => dest.action, opt => opt.MapFrom(src => src.action))
           .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
           .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
           .ForMember(dest => dest.isactive, opt => opt.MapFrom(src => src.isactive));

            CreateMap<CreateBusinessHoursCommand, BusinessHoursDetails>()
          .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
          .ForMember(dest => dest.businessid, opt => opt.MapFrom(src => src.businessid))
          .ForMember(dest => dest.day, opt => opt.MapFrom(src => src.day))
          .ForMember(dest => dest.starttime, opt => opt.MapFrom(src => src.starttime))
          .ForMember(dest => dest.endtime, opt => opt.MapFrom(src => src.endtime))
          .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status));

            CreateMap<UpdateBusinessHoursCommand, BusinessHoursDetails>()
            .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
          .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
          .ForMember(dest => dest.businessid, opt => opt.MapFrom(src => src.businessid))
          .ForMember(dest => dest.day, opt => opt.MapFrom(src => src.day))
          .ForMember(dest => dest.starttime, opt => opt.MapFrom(src => src.starttime))
          .ForMember(dest => dest.endtime, opt => opt.MapFrom(src => src.endtime));

            CreateMap<DeleteBusinessHoursCommand, BusinessHoursDetails>()
           .ForMember(dest => dest.action, opt => opt.MapFrom(src => src.action))
           .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
           .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
           .ForMember(dest => dest.isactive, opt => opt.MapFrom(src => src.isactive));

            CreateMap<UploadBusinessImagesCommand, BusinessImageDetails>()
           .ForMember(dest => dest.BusinessId, opt => opt.MapFrom(src => src.BusinessId))
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
           .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
           .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
           .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName));

            CreateMap<DeleteBusinessImagesCommand, BusinessImageDetails>()
           .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
		   .ForMember(dest => dest.Isactive, opt => opt.MapFrom(src => src.Isactive));

            CreateMap<CreateCommonCommand, CommonDetails>()
           .ForMember(dest => dest.businessid, opt => opt.MapFrom(src => src.businessid))
           .ForMember(dest => dest.count, opt => opt.MapFrom(src => src.count))
           .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username))
           .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type));

            CreateMap<CreateTokenCommand, TokenDetail>()
                .ForMember(dest => dest.token, opt => opt.MapFrom(src => src.token));
        }
    }
}

