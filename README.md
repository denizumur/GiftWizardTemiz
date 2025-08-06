# GiftWizard🧙‍♂️✨

**GiftWizard**, kullanıcı profillerini derinlemesine analiz ederek Google Gemini yapay zeka modeli aracılığıyla kişiye özel ve anlamlı hediye önerileri sunan modern bir web uygulamasıdır. Bu proje, sevdiklerine ne hediye alacağını bilemeyenler için akıllı bir danışman görevi görür.

---
BTK Akademi & Google & Girişimcilik Vakfı iş birliği ile gerçekleştirdiğimiz AI Hackathon yarışması kapsamında hazırlanmıştır.


---

## 🚀 Özellikler

* **Detaylı Profil Analizi:** Kullanıcının yaş aralığı, cinsiyeti, ilgi alanları, kişisel tarzı, bütçesi ve hediye alacağı kişiyle olan ilişkisi gibi birden fazla kritere göre profil oluşturma.
* **Yapay Zeka Destekli Öneriler:** Google'ın güçlü `gemini-1.5-flash` modelini kullanarak her profil için 5 adet yaratıcı ve spesifik hediye fikri üretme.
* **Dinamik Gerekçelendirme:** Sunulan her hediye fikrinin, kullanıcının girdiği profilin hangi özelliklerine dayanılarak önerildiğini açıklayan, ikna edici ve kişiselleştirilmiş gerekçeler sunma.
* **Etkileşimli Web Arayüzü:** Projenin backend'i ile konuşan, vanilya JavaScript ile geliştirilmiş basit, hızlı ve kullanıcı dostu bir frontend.

---

## 🏛️ Mimari Yaklaşım: Clean Architecture

Proje, sürdürülebilir, test edilebilir ve esnek bir kod tabanı oluşturmak amacıyla **Clean Architecture** prensipleri üzerine inşa edilmiştir. Bu sayede iş mantığı, veritabanı ve dış servisler gibi detaylardan tamamen izole edilmiştir.

Proje 4 ana katmandan oluşur:

* **`Domain` Katmanı (Projenin Kalbi):**
    * Uygulamanın temel iş nesnelerini (`Entities` - örn: `GiftProfile`) ve en temel iş kurallarını barındırır.
    * Hiçbir dış bağımlılığı yoktur, tamamen saf C# kodudur.

* **`Application` Katmanı (Projenin Beyni):**
    * Uygulamanın ana iş akışlarını, servis arayüzlerini (`IAiService`, `IGiftFinderService`) ve DTO'ları (Data Transfer Objects) içerir.
    * Sadece `Domain` katmanına bağımlıdır.

* **`Infrastructure` Katmanı (Dış Dünya Bağlantıları):**
    * Veritabanı erişimi, harici API çağrıları (Google Gemini) gibi dış dünya ile ilgili tüm teknik implementasyonları içerir.
    * `Application` katmanındaki arayüzleri uygular.

* **`WebApi` Katmanı (Giriş Kapısı):**
    * Dış dünyadan gelen HTTP isteklerini karşılayan API Controller'larını barındırır.
    * Kullanıcı arayüzünü (`wwwroot`) sunar ve projenin başlangıç yapılandırmasını (`Program.cs`) yönetir.

---

## 🛠️ Kullanılan Teknolojiler

* **Backend:**
    * .NET 8 (LTS)
    * ASP.NET Core Web API
    * C# 12
* **Yapay Zeka:**
    * Google Gemini API (`gemini-1.5-flash`)
    * Doğrudan `HttpClient` ile REST API entegrasyonu
* **Frontend:**
    * HTML5
    * CSS3
    * Vanilla JavaScript (Fetch API)
* **Kaynak Kontrolü:**
    * Git & GitHub

---

## ⚙️ Kurulum ve Başlatma (Lokal Ortam)

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyin:

1.  **.NET 8 SDK'sını yükleyin:**
    * [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

2.  **Projeyi klonlayın:**
    ```bash
    git clone [https://github.com/denizumur/HediyeSihirbaziSon.git](https://github.com/denizumur/HediyeSihirbaziSon.git)
    cd HediyeSihirbaziSon
    ```
    *(Not: Kendi deponuzun URL'si ile güncelleyin)*

3.  **Gerekli sırları yapılandırın (`user-secrets`):**
    Projenin Google Gemini ile konuşabilmesi için API anahtarınıza ve Proje ID'nize ihtiyacı var. Bu bilgileri güvenli bir şekilde saklamak için:

    * Ana proje klasöründeyken terminali açın ve sır kasasını başlatın:
        ```bash
        dotnet user-secrets init --project WebApi
        ```
    * Sırlarınızı ekleyin (`"..."` kısımlarını kendi bilgilerinizle değiştirin):
        ```bash
        dotnet user-secrets set --project WebApi "Gemini:ApiKey" "SENİN_API_ANAHTARIN"
        dotnet user-secrets set --project WebApi "Gemini:ProjectId" "SENİN_GOOGLE_CLOUD_PROJE_ID"
        ```

4.  **Projeyi çalıştırın:**
    ```bash
    dotnet run --project WebApi
    ```

5.  **Tarayıcıda açın:**
    * Terminalde `Now listening on: http://localhost:XXXX` şeklinde bir çıktı göreceksiniz. Bu adresi kopyalayıp tarayıcınızda açın.

---

## 🔑 Yapılandırma (Configuration)

Google Gemini API anahtarı ve Proje ID'si gibi hassas veriler, .NET'in `user-secrets` mekanizması ile yönetilir. Bu sırlar, proje kodundan ayrı tutulur ve kaynak kontrolüne dahil edilmez. Uygulama, bu sırlara `IConfiguration` servisi aracılığıyla erişir.

---

## 📡 API Endpoint'leri

### Hediye Önerisi Al

* **Endpoint:** `POST /api/giftfinder/suggestions`
* **Açıklama:** Verilen profile göre yapay zekadan hediye önerileri ve gerekçeleri alır.
* **Request Body (Örnek):**
    ```json
    {
      "ageRange": "25-35",
      "gender": 1,
      "interests": ["teknoloji", "müzik"],
      "style": "minimalist",
      "occasion": "Doğum Günü",
      "relationship": "Arkadaş",
      "priceRange": "250-750 TL"
    }
    ```
* **Response Body (Örnek):**
    ```json
    [
        {
            "productId": "...",
            "productName": "YÜKSEK KALİTELİ BLUETOOTH KULAKLIK",
            "productImageUrl": "...",
            "price": 650,
            "reasoning": "Teknoloji ve müzik ilgi alanlarını birleştiren bu hediye, minimalist tarzına uygun şık bir tasarıma sahiptir ve belirttiğin bütçe aralığı için harika bir seçimdir."
        }
    ]
    ```

---

## 💡 Gelecek Geliştirmeler

* [ ] **Veritabanı Entegrasyonu:** Önerilen fikirleri gerçek ürünlerle eşleştirmek için bir ürün veritabanı (PostgreSQL, SQL Server vb.) eklemek.
* [ ] **Görsel Analiz:** Kullanıcının yüklediği bir fotoğraftaki stile veya ürüne göre öneriler sunmak.
* [ ] **Kullanıcı Hesapları:** Kullanıcıların kendi profillerini kaydetmesi ve hediye geçmişlerini görmesi.
* [ ] **Daha Gelişmiş Filtreleme:** Önerileri marka, popülerlik, kullanıcı puanı gibi ek filtrelere göre sıralama.
* [ ] **CrewAI Entegrasyonu:** Fiyat takibi yapan, stok kontrolü yapan veya sosyal medya içeriği üreten otonom AI ajanları oluşturmak.

---
