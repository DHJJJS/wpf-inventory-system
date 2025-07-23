# wpf-inventory-system
개인 프로젝트 WPF 기반 실시간 재고관리 시스템

# WPF 재고관리 프로젝트

## 개요
- WPF의 고급 기능을 활용한, TCP 소켓을 통한 클라이언트 실시간 동기화가 가능한 현대적 데스크톱 ERP 시스템

## 기술 스택
- Front : WPF
- (생각 중)

## 개발 진행상황
- [ ] Entity Framework 모델 설계
- [ ] TCP 소켓 서버 구현  
- [ ] WPF 클라이언트 구현
- [ ] 실시간 동기화 구현

## 실행 방법
(나중에 채워넣기)


### 1일차

1. 프로젝트 생성
2. NuGet 패키지 `Microsoft.EntityFrameworkCore.Sqlite` 설치

- .NET Framework 4.7.2(구버전)이라 프레임워크 호환문제 발생 
    -  .NET/.NET Core (크로스플랫폼 - 신버전) 새 프로젝트로 시작

3. 재차 프로젝트 생성 - 패키지 설치
4. 간단한 Produc 클래스 생성 (상품코드, 상품명, 가격, 재고수량)
5. DbContext 클래스

```
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
```

- 해당 코드로 Entity Framework 개념 이해 집중

1. ApplicationDbContext : DbContext

내가 만든 클래스가 Entity Framework의 기능을 빌린다.

2. DbSet<Product> Products

데이터베이스의 Product 테이블을 C# 코드로 다루는 창구.
Products.Add(), Products.Find() 같은 걸로 DB 조작 가능.

3. OnConfiguring

어떤 데이터베이스에 연결할지 설정하는 곳.
여기서 SQLite 파일 경로 지정.