namespace SupermarketReceipt.Test;

public static class VerifyInit
{
    [ModuleInitializer]
    public static void Init()
    {
        Verifier.UseProjectRelativeDirectory("Resources");
    }
}