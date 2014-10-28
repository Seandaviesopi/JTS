// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

#pragma once

#include "GameFramework/Character.h"
#include "PaperFlipbookComponent.h"
#include "JetPlayerMovementComponent.h"
#include "JetPlayerCharacter.generated.h"

/**
 * 
 */
UCLASS()
class JETSTREAMER_API AJetPlayerCharacter : public ACharacter
{
	GENERATED_UCLASS_BODY()

	// Flipbook Component
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Character)
	TSubobjectPtr<class UPaperFlipbookComponent> Flipbook;

	// Camera Component
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera)
	TSubobjectPtr<class UCameraComponent> SideViewCamera;

	// Camera Boom Component 
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera)
	TSubobjectPtr<class USpringArmComponent> CameraBoom;

	// Post Initialize Components
	virtual void PostInitializeComponents() override;

	// Set up player input links
	virtual void SetupPlayerInputComponent(class UInputComponent* InputComponent) override;

	// When true, player wants to use jet pack
	UPROPERTY(BlueprintReadOnly, Category = "Pawn|Character")
	uint32 bEnableJetPack : 1;

protected:

	/*
	*/
	UFUNCTION()
	void OnBeginOverlap(class AActor* OtherActor, class UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult& SweepResult);
	
	/* 
	*/
	void TouchStarted(const ETouchIndex::Type FingerIndex, const FVector Location);

	/* 
	*/
	void TouchStopped(const ETouchIndex::Type FingerIndex, const FVector Location);

	/* Called for side to side input
	 * @param moveAmount - Movement input amount to apply
	*/
	void MoveRight(float moveAmount);

	/*
	 */
	void StartJetpack();

	// Amount of Water Stored
	UPROPERTY()
	uint32 waterResourceAmount;
};
