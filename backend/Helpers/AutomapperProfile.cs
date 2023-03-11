using backend.Dtos.AuthDtos;
using backend.Dtos.CategoryDtos;
using backend.Dtos.SurveyDtos;
using backend.Dtos.SurveyOptionDtos;
using backend.Models;

namespace backend.Helpers;

using AutoMapper;
using Models.IdentityModels;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<RegisterUserDto, User>();
        CreateMap<RefreshTokenDto, TokenDto>();
        CreateMap<AddCategoryDto, Category>();
        CreateMap<Category, CategoryForPageDto>();
        CreateMap<EditCategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
        CreateMap<EditSurveyDto, Survey>();
        CreateMap<AddSurveyOptionDto, SurveyOption>();
    }
}