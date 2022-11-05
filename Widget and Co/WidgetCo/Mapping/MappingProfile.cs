using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Widget_and_Co.Model.DTO;
using Widget_and_Co.Model;
using WidgetCo.Model.Responses;
using AutoMapper;

namespace WidgetCo.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Order, OrderResponse>();              
        }
    }
}
