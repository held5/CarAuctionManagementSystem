# Car Auction Management System

## Project Description

### Overview
The Car Auction Management System is a simple API built in .NET to manage auctions for different types of vehicles: Sedans, SUVs, and Trucks. The system allows users to add vehicles to an auction inventory, search for vehicles, start and close auctions, and place bids on vehicles during active auctions. The focus of the project is on object-oriented design principles, clean and efficient code, and robust error handling.

### Vehicle Types and Attributes
The system handles three types of vehicles, each with its own set of attributes:

- **Sedan**: Number of doors, manufacturer, model, year, and starting bid.
- **SUV**: Number of seats, manufacturer, model, year, and starting bid.
- **Truck**: Load capacity, manufacturer, model, year, and starting bid.

### Key Features
1. **Add Vehicles**: Users can add vehicles to the auction inventory. Each vehicle must have a unique identifier and respective attributes based on its type.
2. **Search Vehicles**: Users can search for vehicles by type, manufacturer, model, or year. The search returns all available vehicles matching the search criteria.
3. **Auction Management**: Users can start and close auctions for vehicles. Only one auction can be active for a vehicle at a time. During an active auction, users can place bids on the vehicle.
4. **Error Handling**: 
   - Ensure unique identifiers for vehicles when adding to inventory.
   - Verify vehicle existence and auction status before starting an auction.
   - Validate active auction status and bid amounts when placing bids.
   - Handle invalid inputs, out-of-range values, and other edge cases.

### Implementation Details
- **Object-Oriented Design**: The system is designed using object-oriented principles, defining classes for vehicles, auctions, and other relevant entities.
- **Auction Operations**: The core operations include adding vehicles, searching vehicles, starting and closing auctions, and placing bids.
- **Unit Testing**: Comprehensive unit tests are included to ensure the correctness of auction management operations.
