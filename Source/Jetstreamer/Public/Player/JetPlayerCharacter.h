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

	/*
	* Add movement input along the given world direction vector (usually normalized) scaled by 'ScaleValue'. If ScaleValue < 0, movement will be in the opposite direction.
	* Base Pawn classes won't automatically apply movement, it's up to the user to do so in a Tick event. Subclasses such as Character and DefaultPawn automatically handle this input and move.
	*
	* @param WorldDirection	Direction in world space to apply input
	* @param ScaleValue		Scale to apply to input. This can be used for analog input, ie a value of 0.5 applies half the normal value, while -1.0 would reverse the direction.
	* @param bForce			If true always add the input, ignoring the result of IsMoveInputIgnored().
	* @see GetPendingMovementInputVector(), GetLastMovementInputVector(), ConsumeMovementInputVector()
	*/
	/*UFUNCTION(BlueprintCallable, Category = "Pawn|Input", meta = (Keywords = "AddInput"))
	virtual void AddMovementInput(FVector WorldDirection, float ScaleValue = 1.0f, bool bForce = false) override;*/

	/* Return our PawnMovementComponent, if we have one. */
	virtual UPawnMovementComponent* GetMovementComponent() const override;

protected:
	
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
};
