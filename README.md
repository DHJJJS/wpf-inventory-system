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

### 2일차

- Entity Framework 개념 이해 추가 집중.

1. DbContext란?
- Entity Framework의 핵심 클래스
- 데이터베이스 전체를 관리하는 "큰 틀" 역할
- 비유: 회사 전체를 관리하는 본사

2. DbSet<T>이란?
- 데이터베이스의 특정 테이블을 관리하는 클래스
- 테이블 하나당 DbSet 하나씩 생성
- 비유: 회사 내의 각 부서 (상품관리부서, 고객관리부서 등)

DbSet의 주요 기능
```csharp
Products.Add(새상품);      // INSERT - "새 상품 등록해줘"
Products.Find(1);         // SELECT by ID - "1번 상품 찾아줘" 
Products.Where(조건);     // SELECT with condition - "조건에 맞는 상품들 찾아줘"
Products.Remove(상품);     // DELETE - "이 상품 삭제해줘"
```

3. Virtual Method와 Override
Virtual Method: 부모 클래스에서 "나중에 자식이 바꿔도 돼"라고 허용한 함수
Override: 자식 클래스에서 "부모 방식 말고 내 방식으로 할래"라고 재정의하는 것

요리 레시피 비유:
```csharp
// 엄마의 기본 레시피 (부모 클래스)
class 기본요리법
{
    virtual void 양념만들기()  // "너가 바꿔도 돼"
    {
        // 기본 양념: 소금, 후추
    }
}

// 내 커스텀 레시피 (자식 클래스)  
class 내요리법 : 기본요리법
{
    override void 양념만들기()  // "내 방식으로 할래"
    {
        // 내 양념: 마늘, 간장, 참기름
    }
}
```

4. OnConfiguring 메서드
- 데이터베이스 연결 설정을 담당하는 함수
- Entity Framework가 "어떤 DB에 연결할래?" 물어볼 때 답해주는 곳
- 비유: 회사 설립할 때 "사무실 주소를 여기로 정하겠다" 선언하는 것

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlite("Data Source=inventory.db");  // SQLite 파일 경로 지정
}
```

## 💻 실제 구현 코드

```csharp
using Microsoft.EntityFrameworkCore;

namespace wpf_inventory_system
{
    // 상품 엔티티 (데이터베이스 테이블과 매핑될 클래스)
    public class Product
    {
        public int ProductId { get; set; }        // 기본키
        public string ProductName { get; set; }   // 상품명
        public int ProductPrice { get; set; }     // 가격
        public int ProductInventory { get; set; } // 재고수량
    }
    
    // 데이터베이스 컨텍스트 (DB 전체 관리)
    public class ApplicationDbContext : DbContext
    {
        // Products 테이블을 관리하는 DbSet
        public DbSet<Product> Products { get; set; }
        
        // 데이터베이스 연결 설정
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }
    }
}
```

### SQLite -> MySQL로 교체 결정

`Microsoft.EntityFrameworkCore.Sqlite` 제거 -> `Pomelo.EntityFrameworkCore.MySql` 설치

코드 추가

```Csharp
optionsBuilder.UseMySql("Server=localhost;Database=inventory;Uid=root;Pwd=root;",
ServerVersion.AutoDetect("Server=localhost;Database=inventory;Uid=root;Pwd=root;")
);
```

#### 트러블 슈팅

발생한 문제

- 빌드 에러:
```
error CS1503: 2 인수: 'string'에서 'Microsoft.EntityFrameworkCore.ServerVersion'(으)로 변환할 수 없습니다.
```

문제가 된 코드:
```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseMySql("Server=localhost;Database=inventory;Uid=root;Pwd=root;");
}
```

원인 분석

- MySQL: 서버 버전 정보 필수 (버전별 기능 차이 존재)

왜 ServerVersion이 필요한가?
1. MySQL 버전별 지원 기능 차이
   - MySQL 5.7 vs 8.0 → 문법, 함수, 기능 다름
2. Entity Framework가 적절한 SQL 생성하기 위해
   - 버전에 맞지 않는 쿼리 → 런타임 에러 발생
3. Pomelo 드라이버의 설계 철학
   - 안전한 쿼리 생성을 위해 버전 정보 강제

해결 방법 - AutoDetect 사용 (추천)
```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);
    
    var connectionString = "Server=localhost;Database=inventory;Uid=root;Pwd=root;";
    optionsBuilder.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
}
```