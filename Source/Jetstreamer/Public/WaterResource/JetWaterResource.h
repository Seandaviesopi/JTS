// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

#pragma once

#include "GameFramework/Actor.h"
#include "PaperFlipbookComponent.h"
#include "JetWaterMovementComponent.h"
#include "JetWaterResource.generated.h"

/* Resource State Enum
 */
UENUM()
enum class EWaterResourceState : uint8
{
	WATER,
	STEAM,
	MAX
};

/* Resource State Data
*/
USTRUCT()
struct FWaterResourceData
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditDefaultsOnly, Category = Default)
	class UPaperFlipbook* flipbookData;
};

/**
 * 
 */
UCLASS()
class JETSTREAMER_API AJetWaterResource : public AActor
{
	GENERATED_UCLASS_BODY()

	// Collision Component of this Actor
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Component)
	TSubobjectPtr<class USphereComponent> resourceCollider;

	// Movement Component of this Actor
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Component)
	TSubobjectPtr<class UJetWaterMovementComponent> resourceMovement;

	// Flipbook Component of this Actor
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Component)
	TSubobjectPtr<class UPaperFlipbookComponent> resourceFlipbook;

	// Post Initialize Components
	virtual void PostInitializeComponents();

protected:
	
	// Array of Resource State Data
	UPROPERTY(EditDefaultsOnly, Category = WaterResource)
	FWaterResourceData waterData[EWaterResourceState::MAX];

	// Current Water State
	UPROPERTY(EditInstanceOnly, Category = WaterResource)
	EWaterResourceState waterState;
	
};
