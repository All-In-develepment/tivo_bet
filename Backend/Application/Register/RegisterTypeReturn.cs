using Domain;

namespace Application.Register
{
    public static class RegisterTypeExten
    {
        public static string RegisterTypeReturn(this RegisterType registerType){
            switch(registerType){
                case RegisterType.Novo:
                    return "Novo";
                case RegisterType.Redeposito:
                    return "Redeposito";
                default:
                    return "Novo";
            }
        }
    }
}