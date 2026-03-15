# Booking Backbone

A small event-driven backend demo exploring a polyglot architecture using .NET and Go.

## Overview

Booking Backbone demonstrates how small services can communicate through events rather than direct calls.

- **booking-api (.NET)** receives booking requests and publishes `booking.created` events  
- **pricing-worker (Go)** processes booking events and emits `booking.priced`  
- **ai-inspector** analyzes the event stream and provides simple operational insights

For demonstration purposes, events are written to a simple JSONL event log.

## Architecture

Client → booking-api (.NET) → booking.created  
pricing-worker (Go) → booking.priced  
ai-inspector → insights

## Project Structure


services/
booking-api
pricing-worker
ai-inspector

shared/
events.jsonl


## Run the API


cd services/booking-api
dotnet run


Swagger will be available at:

/swagger


## Goal

The goal of this project is to experiment with:

- event-driven architecture  
- polyglot backend services  
- simple event processing pipelines