using System;
using System.Collections.Generic;
using System.Text;

namespace WebProjectOOP.Business
{
    public interface IAiService
    {
        Task<string> GenerateDescriptionAsync(string title);

   
    }
}
/* Dependency Inversion prensibi. Yarın bir gün yerel Ollama yerine ChatGPT kullanmak istersek, 
   projenin geri kalanına (Controller vb.) dokunmadan sadece yeni bir servis yazıp bu sözleşmeye bağlayabiliriz.
   Mantığı: Sisteme şunu dedik: "AI servisi ne olursa olsun, bir başlık (title) alır ve geriye asenkron olarak bir açıklama (description) döner." */