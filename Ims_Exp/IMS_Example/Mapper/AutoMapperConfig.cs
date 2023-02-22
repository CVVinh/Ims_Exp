namespace IMS_Example.Mapper
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperCollection(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            return services;
        }
    }
}
