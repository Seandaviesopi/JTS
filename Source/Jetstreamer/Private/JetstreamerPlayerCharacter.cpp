// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

#include "Jetstreamer.h"
#include "JetstreamerPlayerCharacter.h"


AJetstreamerPlayerCharacter::AJetstreamerPlayerCharacter(const class FPostConstructInitializeProperties& PCIP)
	: Super(PCIP)
{
	bUseControllerRotationYaw = true;
	bUseControllerRotationPitch = false;
	bUseControllerRotationRoll = false;

	CameraBoom = PCIP.CreateDefaultSubobject<USpringArmComponent>(this, TEXT("CameraBoom"));
	CameraBoom->AttachTo(RootComponent);
	CameraBoom->bAbsoluteRotation = true;
	CameraBoom->RelativeRotation = FRotator(0.0f, -90.0f, 0.0f);

	SideViewCameraComponent = PCIP.CreateDefaultSubobject<UCameraComponent>(this, TEXT("SideViewCamera"));
	SideViewCameraComponent->ProjectionMode = ECameraProjectionMode::Orthographic;
	SideViewCameraComponent->OrthoWidth = 2048.0f;
	SideViewCameraComponent->AttachTo(CameraBoom, USpringArmComponent::SocketName);

	CameraBoom->bAbsoluteRotation = true;
	SideViewCameraComponent->bUsePawnControlRotation = false;
	CharacterMovement->bOrientRotationToMovement = false;

	CharacterMovement->GravityScale = 2.0f;
	CharacterMovement->AirControl = 0.80f;
	CharacterMovement->JumpZVelocity = 1000.f;
	CharacterMovement->GroundFriction = 3.0f;
	CharacterMovement->MaxWalkSpeed = 600.0f;
	CharacterMovement->MaxFlySpeed = 600.0f;

	CharacterMovement->bConstrainToPlane = true;
	CharacterMovement->SetPlaneConstraintNormal(FVector(0.0f, -1.0f, 0.0f));

	CharacterMovement->bUseFlatBaseForFloorChecks = true;
}

void AJetstreamerPlayerCharacter::SetupPlayerInputComponent(class UInputComponent* InputComponent)
{
	InputComponent->BindAxis("MoveRight", this, &AJetstreamerPlayerCharacter::MoveRight);

	InputComponent->BindAction("MoveUp", IE_Pressed, this, &ACharacter::Jump);
	InputComponent->BindAction("MoveUp", IE_Released, this, &ACharacter::StopJumping);

	InputComponent->BindTouch(IE_Pressed, this, &AJetstreamerPlayerCharacter::TouchStarted);
	InputComponent->BindTouch(IE_Released, this, &AJetstreamerPlayerCharacter::TouchStopped);
}

void AJetstreamerPlayerCharacter::TouchStarted(const ETouchIndex::Type FingerIndex, const FVector Location)
{
	UE_LOG(LogGame, Warning, TEXT("JUMP"));

	Jump();
}

void AJetstreamerPlayerCharacter::TouchStopped(const ETouchIndex::Type FingerIndex, const FVector Location)
{
	StopJumping();
}

void AJetstreamerPlayerCharacter::MoveRight(float moveAmount)
{
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