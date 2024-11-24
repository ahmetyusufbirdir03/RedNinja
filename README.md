# Red Ninja

## Oynanış Bilgileri 
**Red Ninja**, Mario tarzı 2D platform öğeleri içeren bir aksiyon-macera oyunudur. Oyuncunun amacı, düşmanları alt ederek ve engelleri aşarak platformda ilerlemektir. Eğlenceli ve hızlı tempolu oynanışıyla reflekslerinizi ve strateji yeteneklerinizi test etmeyi hedefler.  

## Oynanış  
**Red Ninja**, aşağıdaki temel mekaniklere dayanır:  

### Temel Mekanikler:  
- **Hareket:** Oyuncu, yön tuşları veya `A`, `D` tuşları ile sağa, sola, yukarı ve aşağı hareket edebilir.  
- **Shift ile Hızlı Koşma:** Oyuncu, `Shift` tuşuna basılı tutarak daha hızlı hareket edebilir. Bu mekanik, özellikle zamanla yarışılan bölümlerde veya düşmanlardan kaçarken önemlidir.  
- **Zıplama:** Oyuncu, `Space` tuşuna basarak zıplayabilir. Platformlar arasında ilerlemek ve engelleri aşmak için kullanılır.  
- **Shuriken Fırlatma:** Oyuncu, sol tıklama (`Left Click`) ile shuriken fırlatabilir. Shurikenler düşmanlara zarar vermek için kullanılır ve menzili yok platformun dışına kadar çıkabilir.
- **Checkpointler:** Checkpoint noktaları, oyuncunun ilerlemesini kaydeder. Eğer oyuncu ölürse, en son checkpoint'ten devam eder.  
 
### Platform ve Çevre:  
- **Düşmanlar:** Oyuncunun ilerlemesine engel olmak için hareket eden veya sabit düşmanlar bulunur. Düşmanlar belirli bir hareket modeliyle davranır ve oyuncuyla temas ettiğinde hasar verir.  
- **Sandıklar:** Platform boyunca yerleştirilmiş olan sandıklar, oyuncuya ekstra shuriken, sağlık puanı veya geçici güçlendirmeler sağlar.  
- **Engeller:** Oyuncunun dikkatlice geçmesi gereken çukurlar, hareketli platformlar ve tuzaklar bulunur.  

### Kazanma ve Kaybetme:  
- Oyuncunun hedefi, platformun sonuna ulaşarak tüm düşmanları etkisiz hale getirmek ve bölümü tamamlamaktır.  
- Eğer oyuncu düşmanlar tarafından tüm sağlık puanını kaybeder veya engellerden birine düşerse, oyun en son checkpoint'ten tekrar başlar.  

## Oyuna Erişim  
[Oyun Bağlantısı](#)  

## Grup Üyeleri ve Katkıları  

### 1. **Lütfü Bedel : 21360859030**  
- Aksiyonlar:  
  - **Aksiyom1:Player Movement (A,D) -- PlayerConroller.cs/67 
  - **Aksiyom2:Player Jump -- PlayerConroller.cs/107
  - **Aksiyom3:Oyuncu canı azalma/artma -- PlayerConroller.cs/160
  - **Aksiyom4:Player Fire -- PlayerConroller.cs/142
  - **Aksiyom5: Player double jump -- PlayerConroller.cs/ 95,131
  - **Aksiyom6: Player zemine göre yönünü ayarlama -- PlayerConroller.cs/243

### 2. **Ahmet Yusuf Birdir : 21360859026**  
- Aksiyonlar:
  - **Aksiyom1:Oyuncu shift ile hızlanma -- PlayerController.cs/69 
  - **Aksiyom2:Shark Oyuncuyu takip edip saldırma  -- Shark.cs/89
  - **Aksiyom3:Shark iki nokta arası hareket etme -- Shark.cs/76
  - **Aksiyom4:Slime Fire -- Slime.cs/51
  - **Aksiyom5:Spike / Saw Oyuncuya hasar verme - kuvvet uygulayıp itme -- PlayerController.cs/216,225
  

### 3. **Yusuf Güney : 22360859041**  
- Aksiyonlar:  
  - **Aksiyom1:Moving Platform -- Platform.cs(hepsi)
  - **Aksiyom2:Sallana Gürz -- Mace.cs(hepsi)
  - **Aksiyom3:Checkpoint noktalarına ışınlanma -- Sea.cs(hepsi)
  - **Aksiyom4:Coin toplama/ coinleri ekrana yazdırma -- PlayerController.cs/184,264
  - **Aksiyom5:Helth potion toplama/ oyuncuya can ekleme -- PlayerConroller.cs/208
## Lisans  
Bu proje, MIT Lisansı ile lisanslanmıştır. Daha fazla bilgi için `LICENSE` dosyasını inceleyebilirsiniz.
