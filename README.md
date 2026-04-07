# MCS Software — Travel Agency Management System

MCS Software is a desktop application designed to streamline and automate document workflows for a travel agency.  
The system focuses on managing clients, bookings, payments, and generating structured documents such as vouchers, agreements, and invoices.

## Overview

The goal of this project was to build a structured, real-world business application that handles:

- client data management
- booking organization
- document generation
- payment tracking

The system is designed with a clear workflow where data flows from client → booking → documents → payments, ensuring consistency across all generated outputs.

## Core Features

### Client Management
- Create and edit client profiles
- Store personal and contact information
- Search and select clients from a structured list

### Booking System
- Create bookings linked to clients
- Manage travel details (destination, dates, notes)
- Support multiple passengers per booking

### Document Generation

The system supports multiple document types:

- Voucher
- Agreement
- Invoice

Each document shares a base structure but contains specific fields depending on its type.

### Payment System

The payment model supports flexible real-world scenarios:

- Base reservation price
- Advance payments
- Optional additions:
  - travel cost
  - insurance
  - taxes

Payments can be split across multiple installments, allowing:

- partial payments over time
- full payment tracking until completion
- generation of documents reflecting payment history

### UI State Management

The application uses a structured UI flow:

- No client selected → empty state
- Client selected → view mode
- Editing mode → enabled input fields
- Booking creation → extended form state

Dynamic UI elements include:
- passenger list (add/remove)
- form switching between modes
- controlled input enabling/disabling

## Architecture

The system is structured around clear domain entities:

- Client
- Booking
- Passenger
- Document (base)
  - Voucher
  - Agreement
  - Invoice
- Payment

Each document is linked to:
- a client
- a booking
- a payment record

This ensures consistency across all generated outputs.

## Tech Stack

- C#
- WPF (Windows Presentation Foundation)
- XAML
- .NET

## Design Approach

The focus of this project was not just functionality, but also:

- structuring a real business workflow
- designing reusable document models
- handling state transitions in a desktop UI
- separating data logic from UI interactions

## Challenges Solved

- Managing multiple UI states within a single window
- Designing a flexible payment system with optional components
- Structuring documents with shared and specialized fields
- Dynamically handling lists (e.g. passengers)
- Keeping the UI responsive and predictable during state changes

## Future Improvements

- MVVM refactor for stronger separation of concerns
- Database integration (persistent storage)
- PDF export for documents
- User authentication
- Reporting and analytics
- Cloud synchronization

## What I Learned

- how to design data models for real-world business logic
- how to manage complex UI states in WPF
- how to structure applications around workflows instead of isolated features
- how to think in terms of systems, not just code

## Author

Built by Hristijan Chupetreski.
