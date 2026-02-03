public class OllamaRequest
{
    public string model { get; set; } = "gemma3:4b";
    public string prompt { get; set; }
    public bool stream { get; set; } = false;
    public OllamaOptions options { get; set; } = new OllamaOptions(); // Parametre mühürü
}

public class OllamaOptions
{
    // 0 değeri modeli en ciddi ve tutarlı hale getirir
    public float temperature { get; set; } = 0f;
    public int num_predict { get; set; } = 150; // Cevabı kısa tutar

}

    public class OllamaResponse
{
    public string? response { get; set; }
}