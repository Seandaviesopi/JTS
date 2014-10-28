// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

#include "Jetstreamer.h"
#include "JetWaterResource.h"


AJetWaterResource::AJetWaterResource(const class FPostConstructInitializeProperties& PCIP)
	: Super(PCIP)
{
	// Initialize State
	waterState = EWaterResourceState::WATER;
	//
	resourceCollider = PCIP.CreateDefaultSubobject<USphereComponent>(this, TEXT("Collider"));
	if (resourceCollider != NULL)
	{
		resourceCollider->CanCharacterStepUpOn = ECB_No;
		resourceCollider->bShouldUpdatePhysicsVolume = true;
		RootComponent = resourceCollider;
	}
	//
	resourceMovement = PCIP.CreateDefaultSubobject<UJetWaterMovementComponent>(this, TEXT("Movement"));
	
	//
	resourceFlipbook = PCIP.CreateDefaultSubobject<UPaperFlipbookComponent>(this, TEXT("Flipbook"));
	if (resourceFlipbook != NULL)
	{
		resourceFlipbook->AttachTo(RootComponent);
		
	}
}

void AJetWaterResource::PostInitializeComponents()
{
	Super::PostInitializeComponents();

	if (resourceFlipbook != NULL)
	{
		// Initialize Flipbook to Active State
		resourceFlipbook->SetFlipbook(waterData[(int8)waterState].flipbookData);
	}
}


