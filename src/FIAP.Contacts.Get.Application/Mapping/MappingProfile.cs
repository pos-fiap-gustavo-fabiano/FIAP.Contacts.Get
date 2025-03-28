﻿using AutoMapper;
using FIAP.Contacts.Get.Application.Dto;
using FIAP.Contacts.Get.Domain.ContactAggregate;

namespace FIAP.Contacts.Get.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, ContactWithIdDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
