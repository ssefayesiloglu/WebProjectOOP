using System;
using System.Collections.Generic;
using System.Text;

namespace WebProjectOOP.Entities.Dtos
{
    public class OllamaRequest
    {
        public string model { get; set; } = "llama3";
        public string prompt { get; set; }
        public bool stream {  get; set; }

    }

    public class OllamaResponse
    { 
        public string response { get; set; }  //AI'ın ürettiği asıl metin buraya gelecek.
    }
}
