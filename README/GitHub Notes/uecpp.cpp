// Все классы созданы на основе класса Actor, если не указано другое

// Чтобы добавить новую папку в C++ Classes, нужно указать ее в момент создания скрипта
// Если скрипт уже создан, то открыть папку Windows (не через UE4, его закрыть), переместить вручную скрипт в нужную папку,
// в корневом каталоге ПКМ на файле проекта -> Generate VS Project Files, затем через VS пересобрать решение

// Если проект не запускается из-за ошибки, нужно в VS исправить ошибку и пересобрать проект, либо удалить папки
// .vs, Binaries, Build, Intermediate, Saved и файлы *.sln, *.db, затем запустить *.uproject и нажать OK,
// когда предложат перекомпилировать проект, затем ПКМ на .uproject -> Generate VS Project Files
// Так-же в VS можно запустить debug (F5), для этого автозагружаемым проектом должна быть выбрана игра

// Категории и подкатегории переменных
UPROPERTY(EditAnywhere, Category = "Category1 | Category2")
int32 a;
protected:
    // Если Actor больше не существует на сцене
    virtual void EndPlay(EEndPlayReason::Type Reason) override;

    bool
        int32
        float
        FVector
        FRotator
        FString
        AActor*
        TArray<AActor*>

        // Перечисления
        UENUM()
        enum class EMyClass : uint8
    {
        ONE,
        TWO,
        THREE,
    };

    UCLASS()
        class ROTATION_API AMyActor : public AActor
    {
        GENERATED_BODY()
    public:
        EMyClass MyClass;
    };

    AMyActor::AMyActor()
    {
        MyClass = EMyClass::ONE;
    }

    Super::BeginPlay();     // Вызывает конструктор родительского класса

    UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = "1")
        UPROPERTY(VisibleAnywhere, BlueprintReadWrite, Category = 2)

        UFUNCTION(BlueprintCallable, Category = "1")
        void MyFunc() const;                        // Метод не будет изменять состояние объекта
    UE_LOG(LogTemp, Warning, TEXT("Message"));  // output log

    // screen log, замененный макросом с упрощенной формой
#define print(text) if(GEngine) GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, TEXT(text));
    print("some text");

    // Местоположение игрока
    FVector MyCharacter = GetWorld()->GetFirstPlayerController()->GetPawn()->GetActorLocation();
    if (GEngine)
        GEngine->AddOnScreenDebugMessage(-1, 2.f, FColor::Blue, FString::Printf(TEXT("Location %s"), *MyCharacter.ToString()));

    // Отключение объекта
    SetActorHiddenInGame(true);
    SetActorEnableCollision(false);
    SetActorTickEnabled(false);

    //================ Создание Actor и Mesh component ===============
    // .h
public:
    UPROPERTY()
        USceneComponent* Root;      // Создаем объект

    UPROPERTY(EditAnywhere)
        UStaticMeshComponent* Mesh; // Создаем меш для объекта

    // .cpp
    Root = CreateDefaultSubobject<USceneComponent>(TEXT("Root"));
    RootComponent = Root;

    Mesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Mesh"));
    Mesh->AttachTo(Root);       // Прикрепляем меш к рутовому объекту

//================================ Триггер =======================
// Триггер 1
// Показываем сообщение при входе/выходе из триггера
// C++ класс создавать на основе TriggerBox (или AActor, но в Details вручную добавить BoxCollision)
// .h
protected:
    virtual void BeginPlay() override;

public:
    AMyTriggerBox();

    UFUNCTION()
        void OnOverlapBegin(class AActor* OverlappedActor, class AActor* OtherActor);

    UFUNCTION()
        void OnOverlapEnd(class AActor* OverlappedActor, class AActor* OtherActor);

    // .cpp
#include "DrawDebugHelpers.h"
#include "Engine/Engine.h"

#define print(text) if(GEngine) GEngine->AddOnScreenDebugMessage(-1, 1.5, FColor::Green, text)
#define printf(text, fstring) if(GEngine) GEngine->AddOnScreenDebugMessage(-1, 1.5, FColor::Green, FString::Printf(TEXT(text), fstring))

    AMyTriggerBox::AMyTriggerBox()
    {
        OnActorBeginOverlap.AddDynamic(this, &AMyTriggerBox::OnOverlapBegin);
        OnActorEndOverlap.AddDynamic(this, &AMyTriggerBox::OnOverlapEnd);
    }

    void AMyTriggerBox::BeginPlay()
    {
        Super::BeginPlay();

        // Делаем компонент Box видимым
        DrawDebugBox(GetWorld(), GetActorLocation(), GetComponentsBoundingBox().GetExtent(), FColor::Purple, true, -1, 0, 5);
    }

    void AMyTriggerBox::OnOverlapBegin(AActor * OverlappedActor, AActor * OtherActor)
    {
        if (OtherActor && (OtherActor != this))
        {
            print("Overlap beging");
            printf("Overlapped object name: %s", *OverlappedActor->GetName());
        }
    }

    void AMyTriggerBox::OnOverlapEnd(AActor * OverlappedActor, AActor * OtherActor)
    {
        if (OtherActor && (OtherActor != this))
        {
            print("Overlap end");
            printf("Overlapped object name: %s", *OtherActor->GetName());
        }
    }

    // Триггер 2
    // Триггер взаимодействует лишь с определенным типом Actor
    // Если SpecificActor войдет в триггер, отобразится сообщение, а для другого объекта не отобразится
    // .h
public:
    UPROPERTY(EditAnywhere) class AActor* SpecificActor;

    // .cpp
#include "DrawDebugHelpers.h"
    AMyMyTriggerBox::AMyMyTriggerBox()
    {
        OnActorBeginOverlap.AddDynamic(this, &AMyMyTriggerBox::OnOverlapBegin);
        OnActorEndOverlap.AddDynamic(this, &AMyMyTriggerBox::OnOverlapEnd);
    }

    void AMyMyTriggerBox::BeginPlay()
    {
        Super::BeginPlay();

        DrawDebugBox(GetWorld(), GetActorLocation(), GetComponentsBoundingBox().GetExtent(), FColor::Green, true, -1, 0, 5);
    }

    void AMyMyTriggerBox::OnOverlapBegin(
        class AActor* OverlappedActor,
        class AActor* OtherActor)
    {
        if (OtherActor && (OtherActor != this) && OtherActor == SpecificActor)
            if (GEngine)
                GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, TEXT("Begin"));
    }

    void AMyMyTriggerBox::OnOverlapEnd(class AActor* OverlappedActor, class AActor* OtherActor)
    {
        if (OtherActor && (OtherActor != this) && OtherActor == SpecificActor)
            if (GEngine)
                GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, TEXT("End"));
    }

    //============================= Rotation =========================
    // Rotation 1
    // .h
    UPROPERTY(EditAnywhere, Category = Movement) float AngleAxis;
    UPROPERTY(EditAnywhere, Category = Movement) FVector Dimensions;
    UPROPERTY(EditAnywhere, Category = Movement) FVector AxisVector;
    UPROPERTY(EditAnywhere, Category = Movement) float Multiplier;
    UPROPERTY(EditAnywhere) UStaticMeshComponent* Mesh;
    UPROPERTY() USceneComponent* Root;

    // .cpp
    ARotation::ARotation()
    {
        PrimaryActorTick.bCanEverTick = true;

        Dimensions = FVector(300, 0, 0);
        AxisVector = FVector(0, 0, 1);
        Multiplier = 50.f;

        Root = CreateDefaultSubobject<USceneComponent>(TEXT("Root"));
        RootComponent = Root;

        Mesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Mesh"));
        Mesh->AttachTo(Root);
    }

    void ARotation::Tick(float DeltaTime)
    {
        FVector NewLocation = FVector(0, 0, 400);
        AngleAxis += DeltaTime * Multiplier;
        if (AngleAxis >= 360.0f)
            AngleAxis = 0;
        FVector RotateValue = Dimensions.RotateAngleAxis(AngleAxis, AxisVector);
        NewLocation.X += RotateValue.X;
        NewLocation.Y += RotateValue.Y;
        NewLocation.Z += RotateValue.Z;
        FRotator NewRotation = FRotator(0, AngleAxis, 0);
        FQuat QuatRotation = FQuat(NewRotation);
        SetActorLocationAndRotation(
            NewLocation, QuatRotation, false, 0, ETeleportType::None);
    }

    // Rotation 2
    // .h
    UPROPERTY() float AngleAxis;
    UPROPERTY() USceneComponent* Root;
    UPROPERTY(EditAnywhere) UStaticMeshComponent* Mesh;

    // .cpp
    ARotation::ARotation()
    {
        PrimaryActorTick.bCanEverTick = true;

        AngleAxis = 0;

        Root = CreateDefaultSubobject<USceneComponent>(TEXT("Root"));
        RootComponent = Root;

        Mesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Mesh"));
        Mesh->AttachTo(Root);
    }

    void ARotation::Tick(float DeltaTime)
    {
        Super::Tick(DeltaTime);

        FVector NewLocation = FVector(0, 0, 800);
        FVector Radius = FVector(200, 0, 0);
        AngleAxis++;
        if (AngleAxis > 360.0f)
            AngleAxis = 1;
        FVector RotateValue = Radius.RotateAngleAxis(AngleAxis, FVector(0, 0, 1));
        NewLocation.X += RotateValue.X;
        NewLocation.Y += RotateValue.Y;
        NewLocation.Z += RotateValue.Z;
        SetActorLocation(NewLocation);
    }

    // Rotation 3
    // .h
    UPROPERTY(EditAnywhere, Category = "Rotaion") float pitchValue;
    UPROPERTY(EditAnywhere, Category = "Rotaion") float yawValue;
    UPROPERTY(EditAnywhere, Category = "Rotaion") float rollValue;

    // .cpp
    ARotation::ARotation()
    {
        pitchValue = 0.f;
        yawValue = 0.f;
        rollValue = 0.f;
    }

    void ARotation::Tick(float DeltaTime)
    {
        FQuat QuatRotation = FQuat(FRotator(pitchValue, yawValue, rollValue));
        AddActorLocalRotation(QuatRotation, false, 0, ETeleportType::None);
    }

    // Rotation 4
    // Вращение объекта вокруг игрока
    // .h
    float AngleAxis;
    UPROPERTY(EditAnywhere) FVector Dimensions;
    UPROPERTY(EditAnywhere) FVector AxisVector;
    UPROPERTY(EditAnywhere) float Multiplier;

    // .cpp
    ARotation::ARotation()
    {
        Dimensions = FVector(300, 0, 0);
        AxisVector = FVector(0, 0, 1);
        Multiplier = 50.f;
    }

    void ARotation::Tick(float DeltaTime)
    {
        FVector NewLocation = GetWorld()->GetFirstPlayerController()->GetPawn()->GetActorLocation();
        AngleAxis += DeltaTime * Multiplier;
        if (AngleAxis >= 360.f)
            AngleAxis = 0;

        FVector RotateValue = Dimensions.RotateAngleAxis(AngleAxis, AxisVector);
        NewLocation.X += RotateValue.X;
        NewLocation.Y += RotateValue.Y;
        NewLocation.Z += RotateValue.Z;

        FRotator NewRotation = FRotator(0, AngleAxis, 0);
        FQuat QuatRotation = FQuat(NewRotation);
        SetActorLocationAndRotation(NewLocation, QuatRotation, false, 0, ETeleportType::None);
    }

    //========================== Component hit =======================
    // Если Actor сталкивается с объектом, то на экран выводится имя этого объекта
    // .h
    UPROPERTY(VisibleAnywhere) class UBoxComponent* MyComp;

    UFUNCTION()
        void OnCompHit(
            UPrimitiveComponent * HitComp,
            AActor * OtherActor,
            UPrimitiveComponent * OtherComp,
            FVector NormalImpulse,
            const FHitResult & Hit);

    // .cpp
#include "Components/BoxComponent.h"
#include "Kismet/GameplayStatics.h"
    ARotation::ARotation()
    {
        MyComp = CreateDefaultSubobject<UBoxComponent>(TEXT("BoxComp"));
        MyComp->SetSimulatePhysics(true);
        MyComp->SetNotifyRigidBodyCollision(true);
        MyComp->BodyInstance.SetCollisionProfileName("BlockAllDynamic");
        MyComp->OnComponentHit.AddDynamic(this, &ARotation::OnCompHit);
        RootComponent = MyComp;
    }

    void ARotation::OnCompHit(
        UPrimitiveComponent * HitComp,
        AActor * OtherActor,
        UPrimitiveComponent * OtherComp,
        FVector NormalImpulse,
        const FHitResult & Hit)
    {
        if ((OtherActor != NULL) && (OtherActor != this) && (OtherComp != NULL))
            if (GEngine)
                GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Green, FString::Printf(TEXT("Hit %s"), *OtherActor->GetName()));
    }

    //==================== Перемещение и вращение объекта ============
    // .h
    UPROPERTY(EditAnywhere, Category = "Location") FVector NewLocation;
    UPROPERTY(EditAnywhere, Category = "Location") FQuat NewRotation;

    // .cpp
    void ARotation::BeginPlay()
    {
        SetActorLocationAndRotation(NewLocation, NewRotation, false, 0, ETeleportType::None);
    }

    //===================== Вызов функции по таймеру =================
    // .h
    UFUNCTION()
        void RepeatingFunction();
    FTimerHandle MemberTimerHandle;

    // .cpp
#include "TimerManager.h"
    void ARotation::BeginPlay()
    {
        Super::BeginPlay();

        GetWorldTimerManager().SetTimer(MemberTimerHandle, this, &ARotation::RepeatingFunction, 2.f, true, 5.f);
    }

    void ARotation::RepeatingFunction()
    {
        if (GEngine)
            GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, TEXT("HELLO"));
    }

    //=========================== Нажатие кнопки =====================
    // Нажатие кнопки 1
    // Если игрок внутри триггера, то нажатие на F включает/выключает свет
    //.h
    UPROPERTY(VisibleAnywhere, Category = "Light Switch") class UPointLightComponent* PointLight;
    UPROPERTY(VisibleAnywhere, Category = "Light Switch") class USphereComponent* LightSphere;
    UPROPERTY(VisibleAnywhere, Category = "Light Switch") float LightIntensity;
    UFUNCTION(BlueprintCallable, Category = "Light Switch") void ToggleLight();

    // .cpp
#include "Components/PointLightComponent.h"
#include "Components/SphereComponent.h"

    ARotation::ARotation()
    {
        LightIntensity = 3000.f;
        PointLight = CreateDefaultSubobject<UPointLightComponent>(TEXT("Light"));
        PointLight->Intensity = LightIntensity;
        PointLight->bVisible = true;
        RootComponent = PointLight;

        LightSphere = CreateDefaultSubobject<USphereComponent>(TEXT("Sphere"));
        LightSphere->InitSphereRadius(300.f);
        LightSphere->SetCollisionProfileName(TEXT("Trigger"));
        LightSphere->SetCollisionResponseToChannel(ECC_Pawn, ECR_Ignore);
        LightSphere->SetupAttachment(RootComponent);
    }

    // Character.h
    UCLASS(config = Game)
        class AMyProjectCharacter : public ACharacter
    {
        GENERATED_BODY()
            UPROPERTY(VisibleAnywhere, Category = "Trigger Capsule") class UCapsuleComponent* TriggerCapsule;

    public:
        UFUNCTION()
            void OnOverlapBegin(
                UPrimitiveComponent* OverlappedComp,
                AActor* OtherActor,
                UPrimitiveComponent* OtherComp,
                int32 OtherBodyIndex,
                bool bFromSweep,
                const FHitResult& SweepResult);

        UFUNCTION()
            void OnOverlapEnd(
                UPrimitiveComponent* OverlappedComp,
                AActor* OtherActor,
                UPrimitiveComponent* OtherComp,
                int32 OtherBodyIndex);

        class ARotation* CurrentLightSwitch;

    protected:
        // Вызывается при нажатии на кнопку F
        void OnAction();
    }

    // Character.cpp
#include "Rotation.h" // Так называется класс
    AMyProjectCharacter::AMyProjectCharacter()
    {
        TriggerCapsule = CreateDefaultSubobject<UCapsuleComponent>(TEXT("Trigger"));
        // Размер должен совпадать с размером коллайдера игрока
        TriggerCapsule->InitCapsuleSize(55.f, 96.f);
        TriggerCapsule->SetCollisionProfileName(TEXT("Trigger"));
        TriggerCapsule->SetupAttachment(RootComponent);

        TriggerCapsule->OnComponentBeginOverlap.AddDynamic(this, &AMyProjectCharacter::OnOverlapBegin);
        TriggerCapsule->OnComponentEndOverlap.AddDynamic(this, &AMyProjectCharacter::OnOverlapEnd);

        CurrentLightSwitch = NULL;
    }

    void AMyProjectCharacter::SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent)
    {
        // Вызываем функцию OnAction при нажатии на кнопку F
        PlayerInputComponent->BindAction("Action", IE_Pressed, this, &AMyProjectCharacter::OnAction);
    }

    void AMyProjectCharacter::OnAction()
    {
        if (CurrentLightSwitch)
            CurrentLightSwitch->ToggleLight();
    }

    void AMyProjectCharacter::OnOverlapBegin(
        UPrimitiveComponent * OverlappedComp,
        AActor * OtherActor,
        UPrimitiveComponent * OtherComp,
        int32 OtherBodyIndex,
        bool bFromSweep,
        const FHitResult & SweepResult)
    {
        if (OtherActor &&
            (OtherActor != this) &&
            OtherComp &&
            OtherActor->GetClass()->IsChildOf(ARotation::StaticClass()))
            CurrentLightSwitch = Cast<ARotation>(OtherActor);
    }

    void AMyProjectCharacter::OnOverlapEnd(
        UPrimitiveComponent * OverlappedComp,
        AActor * OtherActor,
        UPrimitiveComponent * OtherComp,
        int32 OtherBodyIndex)
    {
        if (OtherActor && (OtherActor != this) && OtherComp)
            CurrentLightSwitch = NULL;
    }

    // Нажатие кнопки 2
    // Выводим сообщение при нажатии на F
    // Character.h
protected:
    void OnAction();

    // Character.cpp
    void AMyProjectCharacter::SetupPlayerInputComponent(
        class UInputComponent* PlayerInputComponent)
    {
        PlayerInputComponent->BindAction("Action", IE_Pressed, this, &AMyProjectCharacter::OnAction);
    }

    void AMyProjectCharacter::OnAction()
    {
        if (GEngine)
            GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, TEXT("Message"));
    }

    //============================= Force ============================
    // Взрыв гранаты. Физические объекты, находящиеся внутри сферы, разлетятся в стороны
    // .cpp
#include "DrawDebugHelpers.h"
    void ARotation::BeginPlay()
    {
        TArray<FHitResult> OutHits;
        FVector MyLocation = GetActorLocation();
        FCollisionShape MyColSphere = FCollisionShape::MakeSphere(500.f);

        DrawDebugSphere(
            GetWorld(),
            MyLocation,
            MyColSphere.GetSphereRadius(),
            50,
            FColor::Green, true);

        bool isHit = GetWorld()->SweepMultiByChannel(
            OutHits,
            MyLocation,
            MyLocation,
            FQuat::Identity,
            ECC_WorldStatic,
            MyColSphere);

        if (isHit)
            for (auto& Hit : OutHits)
            {
                UStaticMeshComponent* MeshComp =
                    Cast<UStaticMeshComponent>((Hit.GetActor())->GetRootComponent());
                if (MeshComp)
                    MeshComp->AddRadialImpulse(MyLocation, 500.f, 2000.f, ERadialImpulseFalloff::RIF_Constant, true);
            }
    }

    //====================== Destroy on Overlap ======================
    // Уничтожаем объект при входе в сферу
    // .h
public:
    UPROPERTY(VisibleAnywhere) class USphereComponent* MyCollisionSphere;
    UPROPERTY(VisibleAnywhere) class UStaticMeshComponent* MyMesh;

    UFUNCTION()
        void OnOverlapBegin(
            UPrimitiveComponent * OverlappedComp,
            AActor * OtherActor,
            UPrimitiveComponent * OtherComp,
            int32 OtherBodyIndex,
            bool bFromSweep,
            const FHitResult & SweepResult);

    // .cpp
#include "Components/StaticMeshComponent.h"
#include "Components/SphereComponent.h"

    ARotation::ARotation()
    {
        MyCollisionSphere = CreateDefaultSubobject<USphereComponent>(TEXT("Sphere"));
        MyCollisionSphere->InitSphereRadius(1000.f);
        MyCollisionSphere->SetCollisionProfileName("Trigger");
        RootComponent = MyCollisionSphere;
        MyMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Mesh"));
        MyMesh->SetupAttachment(RootComponent);
        MyCollisionSphere->OnComponentBeginOverlap.AddDynamic(
            this, &ARotation::OnOverlapBegin);
    }

    void ARotation::OnOverlapBegin(
        UPrimitiveComponent * OverlappedComp,
        AActor * OtherActor,
        UPrimitiveComponent * OtherComp,
        int32 OtherBodyIndex,
        bool bFromSweep,
        const FHitResult & SweepResult)
    {
        if ((OtherActor != nullptr) && (OtherActor != this) && (OtherComp != nullptr))
            Destroy();
    }

    //====================== Switch Material =========================
    // Если войти/выйти из триггера, материал изменится
    // Создаем два материала
    // .h
    UPROPERTY(VisibleAnywhere) UStaticMeshComponent* MyMesh;
    UPROPERTY(EditAnywhere) class  UMaterialInterface* OffMaterial;
    UPROPERTY(EditAnywhere) class  UMaterialInterface* OnMaterial;
    UPROPERTY() class UBoxComponent* MyBoxComponent;

    UFUNCTION()
        void OnOverlapBegin(
            UPrimitiveComponent * OverlappedComp,
            AActor * OtherActor,
            UPrimitiveComponent * OtherComp,
            int32 OtherBodyIndex,
            bool bFromSweep,
            const FHitResult & SweepResult);

    UFUNCTION()
        void OnOverlapEnd(
            UPrimitiveComponent * OverlappedComp,
            AActor * OtherActor,
            UPrimitiveComponent * OtherComp,
            int32 OtherBodyIndex);

    // .cpp
#include "DrawDebugHelpers.h"
#include "Components/BoxComponent.h"

    ARotation::ARotation()
    {
        MyMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Mesh"));
        RootComponent = MyMesh;

        MyBoxComponent = CreateDefaultSubobject<UBoxComponent>(TEXT("Box"));
        MyBoxComponent->InitBoxExtent(FVector(100, 100, 100));
        MyBoxComponent->SetCollisionProfileName("Trigger");
        MyBoxComponent->SetupAttachment(RootComponent);

        OnMaterial = CreateDefaultSubobject<UMaterial>(TEXT("On Material"));
        OffMaterial = CreateDefaultSubobject<UMaterial>(TEXT("Off Material"));
        MyBoxComponent->OnComponentBeginOverlap.AddDynamic(this, &ARotation::OnOverlapBegin);
        MyBoxComponent->OnComponentEndOverlap.AddDynamic(this, &ARotation::OnOverlapEnd);
    }

    void ARotation::BeginPlay()
    {
        DrawDebugBox(GetWorld(), GetActorLocation(), FVector(100, 100, 100), FColor::White, true, -1, 0, 1.f);
        MyMesh->SetMaterial(0, OffMaterial);
    }

    void ARotation::OnOverlapBegin(
        UPrimitiveComponent * OverlappedComp,
        AActor * OtherActor,
        UPrimitiveComponent * OtherComp,
        int32 OtherBodyIndex,
        bool bFromSweep,
        const FHitResult & SweepResult)
    {
        if ((OtherComp != nullptr) && OtherActor != this)
            MyMesh->SetMaterial(0, OnMaterial);
    }

    void ARotation::OnOverlapEnd(
        UPrimitiveComponent * OverlappedComp,
        AActor * OtherActor,
        UPrimitiveComponent * OtherComp,
        int32 OtherBodyIndex)
    {
        MyMesh->SetMaterial(0, OffMaterial);
    }

    //========================= Открытие двери =======================
    // .h
    UPROPERTY(EditAnywhere) UStaticMeshComponent* Door;
    UPROPERTY(EditAnywhere) UBoxComponent* BoxComponent;
    bool Open;
    float RotateValue;
    FRotator DoorRotation;

    UFUNCTION()
        void OnOverlapBegin(
            UPrimitiveComponent * OverlappedComp,
            AActor * OtherActor,
            UPrimitiveComponent * OtherComp,
            int32 OtherBodyIndex,
            bool bFromSweep,
            const FHitResult & SweepResult);

    UFUNCTION()
        void OnOverlapEnd(
            UPrimitiveComponent * OverlappedComp,
            AActor * OtherActor,
            UPrimitiveComponent * OtherComp,
            int32 OtherBodyIndex);

    // .cpp
#include "Components/BoxComponent.h"
#include "Kismet/KismetMathLibrary.h"

    ARotation::ARotation()
    {
        Open = false;

        BoxComponent = CreateDefaultSubobject<UBoxComponent>(TEXT("Box"));
        BoxComponent->InitBoxExtent(FVector(50.f, 50.f, 50.f));
        RootComponent = BoxComponent;

        Door = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Door"));
        Door->SetRelativeLocation(FVector(0.f, 50.f, -50.f));
        Door->SetupAttachment(RootComponent);

        BoxComponent->OnComponentBeginOverlap.AddDynamic(this, &ARotation::OnOverlapBegin);
        BoxComponent->OnComponentEndOverlap.AddDynamic(this, &ARotation::OnOverlapEnd);
    }

    void ARotation::Tick(float DeltaTime)
    {
        DoorRotation = Door->RelativeRotation;
        if (Open)
            Door->SetRelativeRotation(FMath::Lerp(FQuat(DoorRotation), FQuat(FRotator(0.f, RotateValue, 0.f)), 0.01f));
        else
            Door->SetRelativeRotation(FMath::Lerp(FQuat(DoorRotation), FQuat(FRotator(0.f, 0.f, 0.f)), 0.01f));
    }

    void ARotation::OnOverlapBegin(
        UPrimitiveComponent * OverlappedComp,
        AActor * OtherActor,
        UPrimitiveComponent * OtherComp,
        int32 OtherBodyIndex,
        bool bFromSweep,
        const FHitResult & SweepResult)
    {
        if ((OtherActor != nullptr) &&
            (OtherActor != this) &&
            (OtherComp != nullptr))
        {
            FVector PawnLocation = OtherActor->GetActorLocation();
            FVector Direction = GetActorLocation() - PawnLocation;
            Direction = UKismetMathLibrary::LessLess_VectorRotator(Direction, GetActorRotation());

            if (Direction.X > 0.f)
                RotateValue = 90.f;
            else
                RotateValue = -90.f;

            Open = true;
        }
    }

    void ARotation::OnOverlapEnd(
        UPrimitiveComponent * OverlappedComp,
        AActor * OtherActor,
        UPrimitiveComponent * OtherComp,
        int32 OtherBodyIndex)
    {
        Open = false;
    }

    //=========================== Таймер =============================
    // Игра завершится через 5 секунд после запуска
    // .h
protected:
    virtual void BeginPlay() override;
    void EndGame();
    FTimerHandle GameHandle;

    // .cpp
#include "Kismet/KismetSystemLibrary.h"
#include "TimerManager.h"

    void AMyActor::BeginPlay()
    {
        // Таймер сработает через 5 сек, false означает, что таймер не зациклен
        GetWorldTimerManager().SetTimer(GameHandle, this, &AMyActor::EndGame, 5.f, false);
    }

    void AMyActor::EndGame()
    {
        UKismetSystemLibrary::QuitGame(GetWorld(), 0, EQuitPreference::Quit);
    }