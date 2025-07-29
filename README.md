# wpf-inventory-system
개인 프로젝트 WPF 기반 실시간 재고관리 시스템

# WPF 재고관리 프로젝트

## 개요
- WPF의 고급 기능을 활용한, TCP 소켓을 통한 클라이언트 실시간 동기화가 가능한 현대적 데스크톱 ERP 시스템

## 기술 스택
- **Frontend**: .NET 8 WPF
- **Database**: SQL Server Express (MySQL에서 변경)
- **ORM**: Entity Framework Core
- **Communication**: TCP Socket (예정)
- **Tools**: Visual Studio 2022, Git

## 프로젝트 구조

wpf-inventory-system/
├── wpf-inventory-system/ // 메인 프로젝트
│   ├── Program.cs // 진입점
│   ├── Models/
│   │   └── Product.cs // 상품 엔티티
│   ├── Data/
│   │   └── ApplicationDbContext.cs // DB 컨텍스트
│   └── Migrations/ // DB 마이그레이션 파일들
└── README.md



## 개발 진행상황
- [ ] Entity Framework 모델 설계
- [ ] TCP 소켓 서버 구현  
- [ ] WPF 클라이언트 구현
- [ ] 실시간 동기화 구현

## 실행 방법
(나중에 채워넣기)


## 개발 일기

<details>
<summary>4일차 - WPF UI 구현 및 프로젝트 구조 확립</summary>

### 4일차

#### SQL Server Express 환경 구축 완료

**EF Core Tools 패키지 문제 해결**
- Migration 실행 시 `Microsoft.EntityFrameworkCore.Design` 패키지 누락 오류 발생
- 패키지 설치 후 Migration 성공적으로 실행

**데이터베이스 연결 문제 해결**
- 초기 연결 문자열: `"Server=localhost;Database=inventory;Trusted_Connection=true;"`로 연결 실패
- SSMS에서 Database 'inventory' 생성
- SSMS에서 실제 서버 이름 확인: `DESKTOP-41VA7UE\LOCALDB#00479452`
- 최종 해결: LocalDB 자동 인스턴스 사용
```csharp
optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=inventory;Integrated Security=true;");
```

#### 개발자 기본 실수 경험
**파일 저장 누락 문제**
- 코드 수정 후 Ctrl+S 저장하지 않고 터미널 실행
- 변경사항이 반영되지 않아 계속 같은 오류 발생
- 해결 후 Migration 성공: `done` 메시지 확인

#### 데이터베이스 CRUD 기능 검증

**콘솔 테스트 구현**
```csharp
using var context = new ApplicationDbContext();
var product = new Product
{
    ProductInventory = 1,
    ProductName = "라",
    ProductPrice = 1,
};
context.Products.Add(product);
context.SaveChanges();

var products = context.Products.ToList();
foreach(var p in products)
{
    Console.WriteLine($"상품 : {p.ProductName}, 가격 : {p.ProductPrice}");
}
```

**양방향 데이터 검증 완료**
- C# 콘솔 출력: "라, 1" 확인
- SSMS에서 실제 DB 데이터 저장 확인
- Entity Framework 완전 작동 검증

#### 아키텍처 방향성 결정

**TCP 소켓 서버 vs WPF 직접 연결**
- 실제 ERP 환경 조사 결과:
  - 일반적 ERP (90%): 클라이언트 → DB 직접 연결
  - 고급 ERP (10%): 클라이언트 → 서버 → DB 구조
- **결정**: 실무 중심의 직접 연결 방식 채택
- 이유: 빠른 완성, 실제 환경과 동일, WPF 심화 학습 집중

#### WPF 프로젝트 생성 및 기본 UI 구현

**프로젝트 구조**
```
wpf-inventory-system/
├── wpf-inventory-system/          # 콘솔 프로젝트 (DB 테스트용)
└── UI_inventory/                  # WPF 프로젝트 (메인 UI)
```

**메인 윈도우 레이아웃 설계**
```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>    <!-- 제목 영역 -->
        <RowDefinition Height="Auto"/>    <!-- 버튼 영역 -->
        <RowDefinition Height="1*"/>      <!-- 데이터 영역 -->
    </Grid.RowDefinitions>
    
    <!-- 제목 -->
    <Label Content="재고 관리 시스템" FontSize="24" FontWeight="Bold"/>
    
    <!-- 버튼 그룹 -->
    <StackPanel Grid.Row="1" Orientation="Horizontal" HorizontalAlignment="Center">
        <Button Content="상품추가" Width="100" Height="40" Margin="5"/>
        <Button Content="새로고침" Width="100" Height="40" Margin="5"/>
        <Button Content="삭제" Width="100" Height="40" Margin="5"/>
    </StackPanel>
    
    <!-- 데이터 그리드 -->
    <DataGrid Grid.Row="2" AutoGenerateColumns="True" IsReadOnly="True"/>
</Grid>
```

#### UI 설계 원칙 적용

**체계적인 레이아웃 구조**
- Grid.RowDefinitions으로 영역 분할
- Auto/1* 높이 설정으로 반응형 디자인
- StackPanel을 활용한 버튼 그룹화

**사용자 친화적 디자인**
- 24px 큰 제목으로 명확한 앱 정체성
- 버튼 크기 통일 (100x40)로 일관성 확보
- DataGrid ReadOnly 설정으로 안전성 보장

#### 트러블슈팅 경험

1. **프로젝트 실수 삭제**
   - 개발 중 실수로 프로젝트 파일 삭제
   - README 문서화의 중요성 재확인
   - 빠른 재생성으로 복구 (경험치 상승)

2. **네이밍 컨벤션 혼동**
   - Product (클래스) vs product (변수) 구분
   - 대소문자 구별의 중요성 학습

## 🎯 다음 단계
- [ ] Entity Framework 코드를 WPF 프로젝트로 이식
- [ ] 버튼 이벤트 핸들러 구현
- [ ] DataGrid 데이터 바인딩
- [ ] 상품 추가/수정/삭제 기능 구현

</details>

<details>
<summary> 3일차 - MySQl -> SQL Server 전환 결정 </summary>
### 3일차

#### MySQL → SQL Server 전환 결정

**ERP 실무 환경 고려**
- 실제 ERP 회사 조사 결과:
  - SQL Server (70% - 한국 ERP 회사 대부분)
  - Oracle (20% - 대기업 ERP) 
  - MySQL/MariaDB (10% - 중소기업)
- **온프레미스 서버실** 환경이 90%
- Windows Server + SQL Server 조합이 표준

#### Migration 시도 및 서버 연결 문제

**EF Core CLI 도구 문제**
```
Add-Migration "Add-Migration" cmdlet을 찾을 수 없습니다.
```
- Package Manager Console에서 EF Core Tools 설치 실패
- .NET CLI 사용으로 우회: `dotnet ef migrations add InitialCreate`

**MySQL 서버 연결 이슈**
- Docker 컨테이너와 로컬 MySQL 서버 간 **포트 충돌** (3306)
- 도커 서버 오류로 실행 불가
- 로컬 MySQL 3307 포트로 변경 시도했으나 연결 실패
- MySQL Workbench 연결 설정 혼동 (설정은 3307, 실제 서버는 3306)

#### 최종 해결: NuGet 패키지 교체

**MySQL 패키지 제거 및 SQL Server 패키지 설치**
```bash
Uninstall-Package Pomelo.EntityFrameworkCore.MySql
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

**코드 변경**
```csharp
// 변경 전 (MySQL)
optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

// 변경 후 (SQL Server)
optionsBuilder.UseSqlServer("Server=localhost;Database=inventory;Trusted_Connection=true;");
```

#### 트러블슈팅 과정에서 배운 점

1. **데이터베이스별 특성 차이**
   - SQLite: 파일 기반, 버전 정보 불필요
   - MySQL: 서버 기반, ServerVersion 필수
   - SQL Server: Windows 통합 인증 지원

2. **실무 환경 고려의 중요성**
   - 개발 편의성 < 실제 업계 표준
   - ERP 개발자에게 SQL Server 경험이 더 가치있음

3. **포트 관리 및 서버 환경 구성**
   - Docker와 로컬 서버 간 리소스 충돌
   - 개발 환경 격리의 중요성

## 🎯 다음 단계
- [ ] SQL Server Express 설치
- [ ] 실제 Migration 실행 및 테이블 생성 확인
- [ ] 콘솔에서 기본 CRUD 테스트

</details>

<details>
<summary>2일차 - Entity Framework 개념 학습</summary>

[### 2일차

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

</details>

<details>
<summary>1일차 - 프로젝트 초기 설정</summary>

[### 1일차

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
여기서 SQLite 파일 경로 지정.]

</details>



