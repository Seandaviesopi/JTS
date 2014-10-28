// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

#include "Jetstreamer.h"
#include "JetPlayerCharacter.h"

/*
 *
 */
AJetPlayerCharacter::AJetPlayerCharacter(const class FPostConstructInitializeProperties& PCIP)
	: Super(PCIP
			.SetDefaultSubobjectClass<UJetPlayerMovementComponent>(ACharacter::CharacterMovementComponentName)
			.DoNotCreateDefaultSubobject(ACharacter::MeshComponentName)
			)
{
	// Only Rotate on Yaw
	bUseControllerRotationYaw = true;
	bUseControllerRotationPitch = false;
	bUseControllerRotationRoll = false;
	// Initialize Capsule Component
	if (CapsuleComponent != NULL)
	{
		CapsuleComponent->SetCapsuleHalfHeight(60.f);
	}
	// Initialize Character Movement Component
	if (CharacterMovement != NULL)
	{
		CharacterMovement->bConstrainToPlane = true;
		CharacterMovement->SetPlaneConstraintNormal(FVector(0.0f, -1.0f, 0.f));
	}
	//
	Flipbook = PCIP.CreateOptionalDefaultSubobject<UPaperFlipbookComponent>(this, TEXT("Flipbook"));
	if (Flipbook != NULL)
    {
		Flipbook->AlwaysLoadOnClient = true;
		Flipbook->AlwaysLoadOnServer = true;
		Flipbook->bOwnerNoSee = false;
		Flipbook->bAffectDynamicIndirectLighting = true;
		Flipbook->PrimaryComponentTick.TickGroup = TG_PrePhysics;
		Flipbook->AttachParent = CapsuleComponent;
        static FName CollisionProfileName(TEXT("CharacterMesh"));
		Flipbook->SetCollisionProfileName(CollisionProfileName);
		Flipbook->bGenerateOverlapEvents = false;
    }
	// Initialize Camera Component
	CameraBoom = PCIP.CreateDefaultSubobject<USpringArmComponent>(this, TEXT("CameraBoom"));
	if (CameraBoom != NULL)
	{
		CameraBoom->AttachTo(RootComponent);
		CameraBoom->bAbsoluteRotation = true;
		CameraBoom->RelativeRotation = FRotator(0.0f, -90.0f, 0.0f);
	}
	// Initialize Side View Camera Component
	SideViewCamera = PCIP.CreateDefaultSubobject<UCameraComponent>(this, TEXT("SideViewCamera"));
	if (SideViewCamera != NULL)
	{
		SideViewCamera->AttachTo(CameraBoom, USpringArmComponent::SocketName);
		SideViewCamera->ProjectionMode = ECameraProjectionMode::Orthographic;
		SideViewCamera->OrthoWidth = 2048.0f;
		SideViewCamera->bUsePawnControlRotation = false;
	}
}

void AJetPlayerCharacter::PostInitializeComponents()
{
	Super::PostInitializeComponents();

	if (!IsPendingKill())
	{
		if (Flipbook != NULL)
		{
			if (Flipbook->PrimaryComponentTick.bCanEverTick && CharacterMovement.IsValid())
			{
				Flipbook->PrimaryComponentTick.AddPrerequisite(CharacterMovement, CharacterMovement->PrimaryComponentTick);
			}
		}
	}
}

void AJetPlayerCharacter::SetupPlayerInputComponent(class UInputComponent* InputComponent)
{
	InputComponent->BindAxis("MoveRight", this, &AJetPlayerCharacter::MoveRight);

	InputComponent->BindTouch(IE_Pressed, this, &AJetPlayerCharacter::TouchStarted);
	InputComponent->BindTouch(IE_Released, this, &AJetPlayerCharacter::TouchStopped);
}

UPawnMovementComponent* AJetPlayerCharacter::GetMovementComponent() const
{
	return this->CharacterMovement;
}

void AJetPlayerCharacter::TouchStarted(const ETouchIndex::Type FingerIndex, const FVector Location)
{
	Jump();
}

void AJetPlayerCharacter::TouchStopped(const ETouchIndex::Type FingerIndex, const FVector Location)
{
	StopJumping();
}

void AJetPlayerCharacter::MoveRight(float moveAmount)
{
	if (moveAmount == 0.f)
	{
		return;
	}

	FVector moveDirection = FVector(1.f, 0.f, 0.f);
	
	// Controller Valid
	if (Controller)
	{
		FRotator newRotation = moveAmount > 0.f ? FRotator(0.f, 0.f, 0.f) : FRotator(0.f, 180.f, 0.f);

		Controller->SetControlRotation(newRotation);
	}
	// Add movement in that direction
	AddMovementInput(moveDirection, moveAmount);
}