using TesteDesignPatterns.Builder.InterfacesProduct;

namespace TesteDesignPatterns.Builder
{
    public class Director
    {
        public void MakeCarSport(IBuilder builder,string engine)
        {
            builder.reset();
            builder.setGps();
            builder.setEngine(engine);
        }

        public void MakeCarSuv(IBuilder builder)
        {
            builder.reset();
            builder.setGps();
        }

    }
}
