namespace EM.Extentions;

public static class ExcetionExtention
{
    public static string GetMEssage(this Exception e)
    {
        return e.InnerException != null ? e.InnerException.Message : e.Message;
    }
}