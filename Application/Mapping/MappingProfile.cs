//using Application.DTOs.Requests;
//using Application.DTOs.Response;
//using AutoMapper;
//using Domain.Models;

//namespace Application.Mapping
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            // CreateMap<Source, Destination>();


//            //USER MAPPINGS
//            CreateMap<User, CreateUserRequest>()
//                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(t => t.PhoneNumber))
//                 .ForMember(dto => dto.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}"))
//                 .ForMember(dto => dto.CreatedAt, opt => opt.MapFrom(c => new DateTimeOffset(c.CreatedAt)));

//            CreateMap<User, UserDetailResponse>()
//                .ForMember(dto => dto.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}"));

//            CreateMap<User, UserResponse>()
//                .ForMember(dto => dto.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}"));

//            CreateMap<User, UserProfileResponse>()
//                .ForMember(dto => dto.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}"));

//            CreateMap<User, UserDetailResponse>()
//                .ForMember(dto => dto.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}"));
//            //.ForMember(dto => dto.HasPaid, opt => opt.MapFrom(u => u.Payments != null && u.Payments.Any(p => p.Tab?.IsPaid == true) == true))
//            //.ForMember(dto => dto.HasPaid, opt => opt.MapFrom(u => u.Payments != null && u.Payments?.Any(p => p.Tab != null && p.Tab.IsPaid)));

//            CreateMap<CreateUserRequest, User>();
//            CreateMap<UserDetailResponse, User>(); 
//            CreateMap<UserProfileResponse, User>();
//            CreateMap<UserProfileResponse, User>();

//            //PRODUCT MAPPINGS

//        }
//    }
//}
