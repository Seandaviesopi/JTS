// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

#pragma once

#include "PaperCharacter.h"
#include "JetstreamerPlayerCharacter.generated.h"

/**
 * 
 */
UCLASS()
class JETSTREAMER_API AJetstreamerPlayerCharacter : public APaperCharacter
{
	GENERATED_UCLASS_BODY()

	/** Side camera */
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category=Camera)
	TSubobjectPtr<class UCameraComponent> SideViewCameraComponent;

	/* Camera boom */
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category=Camera)
	TSubobjectPtr<class USpringArmComponent> CameraBoom;

	// Set up player input links
	virtual void SetupPlayerInputComponent(class UInputComponent* InputComponent) override;
	

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
};
