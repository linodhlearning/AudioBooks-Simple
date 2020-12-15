using AudioBooks.Domain;
using AutoMapper;
using AudioBooks.Model;
using System.Linq;
using System;

namespace AudioBooks.Api.Mappings
{
    public class AudiBookProfile : Profile
    {
        public AudiBookProfile()
        {
            CreateMap<AudioBook, AudioBookItemSummaryModel>()
                .ForMember(dest => dest.AudioBookId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CoverImageFileName, opt => opt.MapFrom(src => src.Audio.CoverImage))
                .ForMember(dest => dest.AudioBookName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.AuthorName))
                .ForMember(dest => dest.QuickText, opt => opt.MapFrom(src => src.QuickSummary))
                .ForMember(dest => dest.AudibleLink, opt => opt.MapFrom(src => src.Audio.AudibleLink))
                 .ReverseMap();

            CreateMap<Author, LookupItemModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AuthorName))
                .ReverseMap();
             

            CreateMap<Category, LookupItemModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName)).ReverseMap();
            CreateMap<Publisher, LookupItemModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PublisherName)).ReverseMap();

            CreateMap<AudioBook, AudioBookItemModel>()
            .ForMember(dest => dest.AudioBookId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.QuickText, opt => opt.MapFrom(src => src.QuickSummary))
            .ForMember(dest => dest.AudioBookName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CoverImageFileName, opt => opt.MapFrom(src => src.Audio.CoverImage))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.AuthorName))
            .ForMember(dest => dest.AudibleLink, opt => opt.MapFrom(src => src.Audio.AudibleLink))
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.PublisherName))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Audio.DurationInMinutes))
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ReverseMap()
            .AfterMap(MapCategories);

        }

        private void MapCategories(AudioBookItemModel model, AudioBook audioBook)
        {
            model.Categories = audioBook.AudioBookCategories.Select(c => c.Category.CategoryName).ToList();
        }
    }



}
