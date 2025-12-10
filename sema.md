```mermaid
flowchart TD
    %% --- Genel Ayarlar ---
    classDef success fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20
    classDef fail fill:#ffebee,stroke:#c62828,stroke-width:2px,color:#b71c1c
    classDef warning fill:#fff3e0,stroke:#ef6c00,stroke-width:2px,color:#e65100
    classDef db fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef input fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#4a148c
    classDef choice fill:#e1bee7,stroke:#4a148c,stroke-width:2px,color:#4a148c

    %% --- Başlangıç ---
    Start@{ shape: circle, label: "BAŞLAT" } 
    --> LoginUI@{ shape: manual-input, label: "Kullanıcı Giriş Ekranı" }
    
    LoginUI --> Method{Giriş Yöntemi}
    
    %% ==================================================
    %% SOL KOL: LDAP AKIŞI (Direkt Öğrenci No)
    %% ==================================================
    Method -- LDAP --> AuthLDAP@{ shape: rect, label: "LDAP Doğrulaması\n(Kullanıcı: Öğrenci No)" }
    
    AuthLDAP --> LDAPCheck{LDAP Başarılı mı?}
    LDAPCheck -- Hayır --> ErrAuth
    
    LDAPCheck -- Evet --> DBCheckLDAP@{ shape: cyl, label: "DB Sorgusu:\nÖğrenci No ile Ara" }
    
    DBCheckLDAP --> HasRecordLDAP{Kayıt Var mı?}
    HasRecordLDAP -- Hayır --> ErrNoStudent
    
    %% LDAP Tekil olduğu için direkt birleşme noktasına gider
    HasRecordLDAP -- Evet --> JoinPoint((Birleşme))

    %% ==================================================
    %% SAĞ KOL: E-DEVLET AKIŞI (TCKN -> Olası Çoklu Kayıt)
    %% ==================================================
    Method -- e-Devlet --> AuthEDevlet@{ shape: rect, label: "e-Devlet Kapısı\n(Kullanıcı: TCKN)" }

    AuthEDevlet --> EDCheck{e-Devlet\nBaşarılı mı?}
    EDCheck -- Hayır --> ErrAuth

    EDCheck -- Evet --> DBCheckTCKN@{ shape: cyl, label: "DB Sorgusu:\nTCKN ile Kayıtları Getir" }

    DBCheckTCKN --> RecordCount{Kayıt Sayısı?}

    %% Durum 1: Hiç kayıt yok
    RecordCount -- 0 Kayıt --> ErrNoStudent

    %% Durum 2: Tek Kayıt (Direkt devam)
    RecordCount -- 1 Kayıt --> JoinPoint

    %% Durum 3: Çoklu Kayıt (Örn: Hem Lisans Hem Yüksek Lisans)
    RecordCount -- "Birden Fazla (>1)" --> SelectAccountUI@{ shape: manual-input, label: "HESAP SEÇİM EKRANI\n(Örn: Asem / Suem /Jamyo/Fakulte)" }
    
    SelectAccountUI --> UserSelected[Kullanıcı Hesabı Seçti]
    UserSelected --> JoinPoint

    %% ==================================================
    %% ORTAK AKIŞ (GÜVENLİK VE DURUM)
    %% ==================================================
    JoinPoint --> StatusCheck{SEÇİLEN ÖĞRENCİ\nDURUMU}

    %% 1. AKTİF
    StatusCheck -- Aktif --> UpdateCheck{Zorunlu Güncelleme?}

    UpdateCheck -- Evet --> RedirectUpdate@{ shape: proc, label: "Profil Güncellemeye\nYönlendir" }
    RedirectUpdate --> StopSuccess

    %% --- Durum Kontrolü ---
    UpdateCheck -- Hayır --> ActiveDash@{ shape: hex, label: "TAM ERİŞİM PANELİ" }
    ActiveDash --> StopSuccess@{ shape: dbl-circ, label: "OTURUM AÇILDI" }

    %% 2. DONDURULMUŞ
    StatusCheck -- Dondurulmuş --> FrozenMsg@{ shape: rounded, label: "Uyarı: Kayıt Donduruldu" }
    FrozenMsg --> FrozenDash@{ shape: hex, label: "KISITLI PANEL\n(Sadece Belge)" }
    FrozenDash --> StopSuccess

    %% 3. MEZUN
    StatusCheck -- Mezun --> GradMsg@{ shape: rounded, label: "Uyarı: Mezun Durumu" }
    GradMsg --> GradDash@{ shape: hex, label: "MEZUN PANELİ\n(Transkript)" }
    GradDash --> StopSuccess

    %% 4. SİLİNMİŞ
    StatusCheck -- Silinmiş --> ErrDeleted@{ shape: odd, label: "Hata:\nErişim Engellendi" }
    
    %% --- Hata Mesajları ---
    ErrAuth@{ shape: odd, label: "Hata:\nDoğrulama Başarısız" } --> StopFail@{ shape: dbl-circ, label: "GİRİŞ REDDİ" }
    ErrNoStudent@{ shape: odd, label: "Hata:\nSistemde Kayıt Yok" } --> StopFail
    ErrDeleted --> StopFail

    %% --- Sınıf Atamaları ---
    class Start,StopSuccess,UserSelected success
    class StopFail,ErrAuth,ErrNoStudent,ErrDeleted fail
    class FrozenMsg,GradMsg warning
    class DBCheckLDAP,DBCheckTCKN db
    class LoginUI,SelectAccountUI input
    class SelectAccountUI choice
    