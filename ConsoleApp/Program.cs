﻿
using Grpc.Net.Client;
using GrpcProtos;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
       {
            bool loop = true;
            Console.WriteLine("Press any key to start the connection\n");
            Console.ReadKey();

            Console.WriteLine("Connecting...\n");
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:5094", new GrpcChannelOptions { HttpHandler = httpHandler });
            if (channel is null)
            {
                Console.WriteLine("Cannot connect\n");
                channel.Dispose();
                return;
            }

            object? allObjects;
            object Object;
            while (loop is true)
            {
                Console.WriteLine("Options: \n" +
                "1 - Create \n" +
                "2 - Get \n" +
                "3 - Finish conection \n");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Select the data type: \n" +
                            "1 - Building \n" +
                            "2 - Floor \n" +
                            "3 - Room \n" +
                            "4 - Variable \n" +
                            "5 - Sample \n");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                CreateBuilding(channel);
                                break;

                            case "2":
                                CreateFloor(channel);
                                break;

                            case "3":
                                CreateRoom(channel);
                                break;

                            case "4":
                                CreateVariable(channel);
                                break;

                            case "5":
                                CreateSample(channel);
                                break;

                            default:
                                Console.WriteLine("Invalid input");
                                break;
                        } //Se crea el objeto 
                        break;

                    case "2":
                        Console.WriteLine("Select the data type: \n" +
                           "1 - Building \n" +
                           "2 - Floor \n" +
                           "3 - Room \n" +
                           "4 - Variable \n" +
                           "5 - Sample \n");

                        string? DataTypeSelection = Console.ReadLine();
                        string? SampleTypeSelection = null;

                        switch (DataTypeSelection)
                        {
                            case "1":
                                allObjects = GetAllBuildings(channel);
                                break;

                            case "2":
                                allObjects = GetAllFloors(channel);
                                break;

                            case "3":
                                allObjects = GetAllRooms(channel);
                                break;

                            case "4":
                                allObjects = GetAllVariables(channel);
                                break;

                            case "5":
                                Console.WriteLine("What kind of sample do you want to get?: \n" +
                                            "1 - Int\n" +
                                            "2 - Double\n" +
                                            "3 - Bool\n");
                                SampleTypeSelection = Console.ReadLine();

                                Console.WriteLine("Do you want to search by: \n" +
                                    "1 - Variable \n" +
                                    "2 - Timespan \n" +
                                    "3 - Get all \n");

                                switch(Console.ReadLine())
                                {
                                    case "1":
                                         switch(SampleTypeSelection)
                                        {
                                            case "1":
                                                allObjects = GetSampleIntByVariableId(channel);
                                                break;

                                            case "2":
                                                allObjects = GetSampleDoubleByVariableId(channel);
                                                break;

                                            case "3":
                                                allObjects = GetSampleBoolByVariableId(channel);
                                                break;

                                            default:
                                                Console.WriteLine("Invalid input");
                                                break;
                                        }//Muestra todos los samples int, double o bool de una variable
                                        break;

                                    case "2":
                                        switch(SampleTypeSelection)
                                        {
                                            case "1":
                                                allObjects = GetSampleIntByTimeSpan(channel);
                                                break;

                                            case "2":
                                                allObjects = GetSampleDoubleByTimeSpan(channel);
                                                break;

                                            case "3":
                                                allObjects = GetSampleBoolByTimeSpan(channel);
                                                break;

                                            default:
                                                Console.WriteLine("Invalid input");
                                                break;
                                        }//Muestra todos los samples int, double o bool en un intervalo de tiempo
                                        break;

                                    case "3":
                                        switch (SampleTypeSelection)
                                        {
                                            case "1":
                                                allObjects = GetAllSampleInts(channel);
                                                break;

                                            case "2":
                                                allObjects = GetAllSampleDoubles(channel);
                                                break;

                                            case "3":
                                                allObjects = GetAllSampleBools(channel);
                                                break;

                                            default:
                                                Console.WriteLine("Invalid input");
                                                break;
                                        }//Muestra todos los samples int, double o bool
                                        break;

                                    default:
                                        Console.WriteLine("Invalid input");
                                        break;
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid input");
                                break;
                        }// Muestra todos los objetos de un tipo

                        Console.WriteLine("Options: \n" +
                            "1 - Select \n" +
                            "2 - Return to main menu \n");

                        switch(Console.ReadLine())
                        {
                            case "1":
                                switch(DataTypeSelection)
                                {
                                    case "1":
                                        Object = GetBuilding(channel, allObjects as Buildings);
                                        break;

                                    case "2":
                                        Object = GetFloor(channel, allObjects as Floors);
                                        break;

                                    case "3":
                                        Object = GetRoom(channel, allObjects as Rooms);
                                        break;

                                    case "4":
                                        Object = GetVariable(channel, allObjects as Variables);
                                        break;

                                    case "5":
                                       switch(SampleTypeSelection)
                                       {
                                            case "1":
                                                Object = GetSampleInt(channel, allObjects as SampleInts);
                                                break;
                                            case "2":
                                                Object = GetSampleDouble(channel, allObjects as SampleDoubles);
                                                break;
                                            case "3":
                                                Object = GetSampleBool(channel, allObjects as SampleBools);
                                                break;
                                       }
                                        break;

                                    default:
                                        Console.WriteLine("Invalid input");
                                        break;
                                }
                                break;

                            case "2":
                                break;
                        }//Muestra la informacion del objeto seleccionado

                        Console.WriteLine("Options: \n" +
                            "1 - Update \n" +
                            "2 - Delete \n" +
                            "3 - Return to main menu");

                        switch(Console.ReadLine())
                        {
                            case "1":
                                switch (DataTypeSelection)
                                {
                                    case "1":
                                        UpdateBuilding(channel , Object);
                                        break;

                                    case "2":
                                        UpdateFloor(channel, Object);
                                        break;

                                    case "3":
                                        UpdateRoom(channel, Object);
                                        break;

                                    case "4":
                                        UpdateVariable(channel, Object);
                                        break;

                                    case "5":
                                        switch(SampleTypeSelection)
                                        {
                                            case "1":
                                                UpdateSampleInt(channel, Object);
                                                break;

                                            case "2":
                                                UpdateSampleDouble(channel, Object);
                                                break;

                                            case "3":
                                                UpdateSampleBool(channel, Object);
                                                break;

                                            default:
                                                Console.WriteLine("Invalid input");
                                                break;
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Invalid input");
                                        break;
                                }//Se actualizan los objetos
                                break;

                            case "2":
                                switch (DataTypeSelection)
                                {
                                    case "1":
                                        DeleteBuilding(channel, Object);
                                        break;

                                    case "2":
                                        DeleteFloor(channel, Object);
                                        break;

                                    case "3":
                                        DeleteRoom(channel, Object);
                                        break;

                                    case "4":
                                        DeleteVariable(channel, Object);
                                        break;

                                    case "5":
                                        switch (SampleTypeSelection)
                                        {
                                            case "1":
                                                DeleteSampleInt(channel, Object);
                                                break;

                                            case "2":
                                                DeleteSampleDouble(channel, Object);
                                                break;

                                            case "3":
                                                DeleteSampleBool(channel, Object);
                                                break;

                                            default:
                                                Console.WriteLine("Invalid input");
                                                break;
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Invalid input");
                                        break;
                                }//Se eliminan los objetos
                                break;

                            case "3":
                                break;

                            default:
                                Console.WriteLine("Invalid input");
                                break;
                        }
                        break;

                    case "3":
                        channel.Dispose();
                        return;

                    case "4":
                        Console.WriteLine("Invalid action");
                        break;
                }
            }
        }
        public static void CreateBuilding(GrpcChannel channel)
        {
            var buildingClient = new Building.BuildingClient(channel);
            Console.Write("Insert the following data: \n" +
                           "Address: ");
            var address = Console.ReadLine();
            Console.Write("\nNumber: ");
            var number = Convert.ToInt32(Console.ReadLine());
            var createResponse = buildingClient.CreateBuilding(new CreateBuildingRequest()
            {
                Address = address,
                Number = number,
            });

            if (createResponse is null)
            {
                Console.WriteLine("Cannot create object");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Successful operation.");
            }
        }

        public static void CreateFloor(GrpcChannel channel)
        {
            var floorClient = new Floor.FloorClient(channel);

            Console.Write("Insert the following data: \n" +
                "Location: ");
            var location = Console.ReadLine();

            Console.WriteLine("\nSelect the corresponding building: \n");

            var allBuildings = GetAllBuildings(channel);
            int position = Convert.ToInt32(Console.ReadLine()) - 1;
            if (position > allBuildings.Items.Count || position < 0)
            {
                Console.Write("Input Out of Range");
                return;
            }

            var createResponse = floorClient.CreateFloor(new CreateFloorRequest()
            {
                Location = location,
                Building = allBuildings.Items[Convert.ToInt32(Console.ReadLine()) - 1]
            });


            if (createResponse is null)
            {
                Console.WriteLine("Cannot create floor");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Successful operation.");
            }
        }

        public static void CreateRoom(GrpcChannel channel)
        {
            var roomClient = new Room.RoomClient(channel);
            var buildingClient = new Building.BuildingClient(channel);

            Console.Write("Insert the following data: \n" +
                "Number: ");
            var roomNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("\n Description: ");
            var description = Console.ReadLine();

            Console.Write("\n Is a production room or an office ? \n" +
                "1 - Production \n" +
                "2 - Office\n");

            bool isProduction;

            if (Console.ReadLine() == "1")
            {
                isProduction = true;
            }
            else 
            {
                isProduction = false;
            }

            Console.WriteLine("Select the corresponding floor: \n");
            var allFloors = GetAllFloors(channel);
            
            int position = Convert.ToInt32(Console.ReadLine()) - 1;
            if (position > allFloors.Items.Count || position < 0)
            {
                Console.Write("Input Out of Range");
                return;
            }
            var buildingOfFloor = buildingClient.GetBuilding(new GetRequest() { Id = allFloors.Items[position].BuildingId });
            var floorOfRoom = allFloors.Items[position];
            floorOfRoom.Building = buildingOfFloor.Building;
            var createResponse = roomClient.CreateRoom(new CreateRoomRequest()
            {
                Number = roomNumber,
                Description = description,
                IsProduction = isProduction,
                Floor = floorOfRoom
                
            });

            if (createResponse is null)
            {
                Console.WriteLine("Cannot create room");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Successful operation.");
            }
        }

        public static void CreateVariable(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var floorClient = new Floor.FloorClient(channel);
            var buildingClient = new Building.BuildingClient(channel);

            Console.WriteLine("Insert the following data: \n" +
                "Code: ");
            var code = Console.ReadLine();

            Console.WriteLine("\n Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("\n Measurement unit: ");
            var measurementUnit = Console.ReadLine();

            Console.WriteLine("\n Where is located? \n" +
                "1 - Building \n" +
                "2 - Floor \n" +
                "3 - Room \n");

            VariableDTO createResponse = null;

            //Lista de Edificios, pisos o habitaciones para escoger la ubicacion de la variable
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Select the corresponding building: \n");
                    var allBuildings = GetAllBuildings(channel);
                    var buildingOfVariable = allBuildings.Items[Convert.ToInt32(Console.ReadLine()) - 1];
                    createResponse = variableClient.CreateVariable(new GrpcProtos.CreateVariableRequest()
                    {
                        Code = code,
                        VariableType = new VariableType() { Name = name, MeasurementUnit = measurementUnit },
                        Building = buildingOfVariable
                    });
                    break;

                case "2":

                    Console.WriteLine("Select the corresponding floor: \n");
                    var allFloors = GetAllFloors(channel);
                    var floorOfVariable = allFloors.Items[Convert.ToInt32(Console.ReadLine()) - 1];
                    var buildingOfFloor = buildingClient.GetBuilding(new GetRequest() { Id = floorOfVariable.BuildingId });
                    floorOfVariable.Building = buildingOfFloor.Building;
                    createResponse = variableClient.CreateVariable(new GrpcProtos.CreateVariableRequest()
                    {
                        Code = code,
                        VariableType = new VariableType() { Name = name, MeasurementUnit = measurementUnit },
                        Floor = floorOfVariable
                    });
                    break;

                case "3":

                    Console.WriteLine("Select the corresponding room: \n");
                    var allRooms = GetAllRooms(channel);
                    var roomOfVariable = allRooms.Items[Convert.ToInt32(Console.ReadLine()) - 1];
                    var floorOfRoom = floorClient.GetFloor(new GetRequest() { Id = roomOfVariable.FloorId });
                    var buildingOfRoom = buildingClient.GetBuilding(new GetRequest() { Id = floorOfRoom.Floor.BuildingId });
                    roomOfVariable.Floor = floorOfRoom.Floor;
                    roomOfVariable.Floor.Building = buildingOfRoom.Building;

                    createResponse = variableClient.CreateVariable(new GrpcProtos.CreateVariableRequest()
                    {
                        Code = code,
                        VariableType = new VariableType() { Name = name, MeasurementUnit = measurementUnit },
                        Room = roomOfVariable
                    });
                    break;

                default:
                    Console.WriteLine("Invalid action");
                    break;
            }

            if (createResponse is null)
            {
                Console.WriteLine("Cannot create variable");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa.");
            }
        }

        public static void CreateSample(GrpcChannel channel)
        {

            var sampleIntClient = new SampleInt.SampleIntClient(channel);
            var sampleDoubleClient = new SampleDouble.SampleDoubleClient(channel);
            var sampleBoolClient = new SampleBool.SampleBoolClient(channel);

            Console.WriteLine("Select the Variable: \n");

            var allVariables = GetAllVariables(channel);

            string variableId = allVariables.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id;

            Console.WriteLine("Select the Sample DataType: \n" +
                "1 - Int\n" +
                "2 - Double\n" +
                "3 - Boolean\n");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Insert Value: ");
                    var createResponseInt = sampleIntClient.CreateSampleInt(new CreateSampleIntRequest()
                    {
                        VariableId = variableId,
                        Value = Convert.ToInt32(Console.ReadLine())
                    });

                    if (createResponseInt is null)
                    {
                        Console.WriteLine("Cannot create variable");
                        channel.Dispose();
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Creación exitosa.");
                    }
                    break;

                case "2":
                    Console.WriteLine("Insert Value: ");
                    var createResponseDouble = sampleDoubleClient.CreateSampleDouble(new CreateSampleDoubleRequest()
                    {
                        VariableId = variableId,
                        Value = Convert.ToDouble(Console.ReadLine())
                    });

                    if (createResponseDouble is null)
                    {
                        Console.WriteLine("Cannot create variable");
                        channel.Dispose();
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Creación exitosa.");
                    }
                    break;

                case "3":
                    Console.WriteLine("Insert Value (true/false): ");
                    var createResponseBool = sampleBoolClient.CreateSampleBool(new CreateSampleBoolRequest()
                    {
                        VariableId = variableId,
                        Value = Convert.ToBoolean(Console.ReadLine())
                    });

                    if (createResponseBool is null)
                    {
                        Console.WriteLine("Cannot create variable");
                        channel.Dispose();
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Creación exitosa.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid Input\n");
                    break;
            }
        }

        public static void UpdateBuilding(GrpcChannel channel)
        {
            var buildingClient = new Building.BuildingClient(channel);
            Console.WriteLine("Select the corresponding building \n");
            var allBuildings = GetAllBuildings(channel);
            int position = Convert.ToInt32(Console.ReadLine()) - 1;
            if (position > allBuildings.Items.Count || position < 0)
            {
                Console.WriteLine("Input Out of Range");
                return;
            }
            var buildingToUpdate = buildingClient.GetBuilding(new GetRequest() { Id = allBuildings.Items[position].Id });
            bool loop = true;

            while (loop)
            { 
                Console.WriteLine("What do you want to modify? \n" +
                    "Number: " + buildingToUpdate.Building.Number + "\n" +
                    "Address: " + buildingToUpdate.Building.Address + "\n" +
                    "Write 1 for Number or 2 for Address\n" +
                    "Press 3 to save");

                // Se modifica el numero o la direccion.
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Write the new number \n");
                        buildingToUpdate.Building.Number = Convert.ToInt32(Console.ReadLine());
                        break;

                    case "2":
                        Console.WriteLine("Write the new Address \n");
                        buildingToUpdate.Building.Address = Console.ReadLine();
                        break;

                    case "3":
                        loop = false;
                        break;

                    default:
                        Console.WriteLine("Invalid action");
                        break;
                }
            }
            buildingClient.UpdateBuilding(buildingToUpdate.Building);

            var updatedGetResponse = buildingClient.GetBuilding(new GetRequest() { Id = buildingToUpdate.Building.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableBuildingDTO.KindOneofCase.Building &&
                updatedGetResponse.Building.Number == buildingToUpdate.Building.Number &&
                updatedGetResponse.Building.Address == buildingToUpdate.Building.Address)
            {
                Console.WriteLine($"Modificación exitosa.");
            }
        }

        public static void UpdateFloor(GrpcChannel channel)
        {
            var floorClient = new Floor.FloorClient(channel);
            var buildingClient = new Building.BuildingClient(channel);

            Console.WriteLine("Select the corresponding floor \n");
            var allFloors = GetAllFloors(channel);
            int position = Convert.ToInt32(Console.ReadLine()) - 1;
            if (position > allFloors.Items.Count - 1  || position < 0)
            {
                Console.WriteLine("Input Out of Range");
                return;
            }
            var floorToUpdate = floorClient.GetFloor(new GetRequest() { Id = allFloors.Items[position].Id });

            bool loop = true;

            while (loop)
            {

                var building = buildingClient.GetBuilding(new GetRequest() { Id = floorToUpdate.Floor.BuildingId });

                Console.WriteLine("What do you want to modify? \n" +
                    "Location: " + floorToUpdate.Floor.Location + "\n" +
                    "Building Number: " + building.Building.Number + "\n" +
                    "Building Address: " + building.Building.Address + "\n" +
                    "Write 1 for Location, 2 for Building\n" +
                    "Press 3 to save");

                //Se modifica la locacion del piso o el edifico al que esta asosiado.
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Write the new Location \n");
                        floorToUpdate.Floor.Location = Console.ReadLine();
                        floorToUpdate.Floor.Building = building.Building;
                        break;

                    case "2":
                        Console.WriteLine("Select the corresponding building \n");
                        var allBuildings = GetAllBuildings(channel);
                        floorToUpdate.Floor.Building = allBuildings.Items[Convert.ToInt32(Console.ReadLine()) - 1];
                        floorToUpdate.Floor.BuildingId = floorToUpdate.Floor.Building.Id;
                        break;

                    case"3":
                        loop = false;
                        break;

                    default:
                        Console.WriteLine("Invalid action");
                        break;
                }
            }
            
            floorClient.UpdateFloor(floorToUpdate.Floor);

            var updatedGetResponse = floorClient.GetFloor(new GetRequest() { Id = floorToUpdate.Floor.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableFloorDTO.KindOneofCase.Floor &&
                updatedGetResponse.Floor.Location == floorToUpdate.Floor.Location &&
                updatedGetResponse.Floor.BuildingId == floorToUpdate.Floor.BuildingId)
            {
                Console.WriteLine($"Modificación exitosa.");
            }
        }

        public static void UpdateRoom(GrpcChannel channel)
        {
            var buildingClient = new Building.BuildingClient(channel);
            var floorClient = new Floor.FloorClient(channel);
            var roomClient = new Room.RoomClient(channel);
            Console.WriteLine("Select the corresponding room: \n");
            var allRooms = GetAllRooms(channel);

            var roomToUpdate = roomClient.GetRoom(new GetRequest() { Id = allRooms.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });


            bool loop = true;

            while (loop)
            {
                var floorOfRoom = floorClient.GetFloor(new GetRequest() { Id = roomToUpdate.Room.FloorId});
                var buildingOfFloor = buildingClient.GetBuilding(new GetRequest() { Id = floorOfRoom.Floor.BuildingId });
                
                if (roomToUpdate.Room.IsProduction is true)
                {
                    Console.WriteLine("What do you want to modify? \n" +
                    "Number: " + roomToUpdate.Room.Number + "\n" +
                    "Description: " + roomToUpdate.Room.Description + "\n" +
                    "Floor Location: " + floorOfRoom.Floor.Location + "\n" +
                    "Building Number: " + buildingOfFloor.Building.Number + "\n" +
                    "Building Address: " + buildingOfFloor.Building.Address + "\n" +
                    "Type: Production \n");
                }
                else
                {
                    Console.WriteLine("What do you want to modify? \n" +
                    "Number: " + roomToUpdate.Room.Number + "\n" +
                    "Description: " + roomToUpdate.Room.Description + "\n" +
                    "Floor Location: " + floorOfRoom.Floor.Location + "\n" +
                    "Building Number: " + buildingOfFloor.Building.Number + "\n" +
                    "Building Address: " + buildingOfFloor.Building.Address + "\n" +
                    "Type: Office \n");
                }
                Console.WriteLine("Write 1 for Number, 2 for Description or 3 for Floor\n" +
                    "Press 4 to save");

                //Se modifica la descripcion de la habitacion, 
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Write the new room number \n");
                        roomToUpdate.Room.Number = Convert.ToInt32(Console.ReadLine());
                        break;


                    case "2":
                        Console.WriteLine("Write the new description \n");
                        roomToUpdate.Room.Description = Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine("Select the corresponding floor \n");
                        var allFloors = GetAllFloors(channel);
                        roomToUpdate.Room.Floor = allFloors.Items[Convert.ToInt32(Console.ReadLine()) - 1];
                        roomToUpdate.Room.FloorId = roomToUpdate.Room.Floor.Id;
                        break;

                    case "4":
                        roomToUpdate.Room.Floor = floorOfRoom.Floor;
                        roomToUpdate.Room.Floor.Building = buildingOfFloor.Building;
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid action");
                        break;
                }
            }
                
            
            roomClient.UpdateRoom(roomToUpdate.Room);

            var updatedGetResponse = roomClient.GetRoom(new GetRequest() { Id = roomToUpdate.Room.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableRoomDTO.KindOneofCase.Room &&
                updatedGetResponse.Room.Description == roomToUpdate.Room.Description &&
                updatedGetResponse.Room.Floor == roomToUpdate.Room.Floor &&
                updatedGetResponse.Room.Floor.Building == roomToUpdate.Room.Floor.Building)
            {
                Console.WriteLine($"Modificación exitosa.");
            }

        }

        public static void UpdateVariable(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var buildingClient = new Building.BuildingClient(channel);
            var floorClient = new Floor.FloorClient(channel);
            Console.WriteLine("Select the corresponding variable: \n");
            var allVariables = GetAllVariables(channel);

            var variableToUpdate = variableClient.GetVariable(new GetRequest() { Id = allVariables.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });

            bool loop = true;

            while (loop)
            {
                Console.WriteLine("What do you want to modify? \n" +
                "Code: " + variableToUpdate.Variable.Code + "\n" +
                "Name: " + variableToUpdate.Variable.VariableType.Name + "\n" +
                "Measurement unit: " + variableToUpdate.Variable.VariableType.MeasurementUnit + "\n");
                if (variableToUpdate.Variable.LocationCase is VariableDTO.LocationOneofCase.Building)
                {
                    Console.WriteLine("Location: Building No." + variableToUpdate.Variable.Building.Number.ToString() + "\n");
                }
                else if (variableToUpdate.Variable.LocationCase is VariableDTO.LocationOneofCase.Floor)
                {
                    Console.WriteLine("Location: " + variableToUpdate.Variable.Floor.Location + " of Building No." + variableToUpdate.Variable.Floor.Building.Number.ToString() + "\n");
                }
                else if (variableToUpdate.Variable.LocationCase is VariableDTO.LocationOneofCase.Room)
                {
                    Console.WriteLine("Location: Room No." + variableToUpdate.Variable.Room.Number.ToString() + " of " + variableToUpdate.Variable.Room.Floor.Location + " of Building No." + variableToUpdate.Variable.Room.Floor.Building.Number.ToString() + "\n");
                }


                Console.WriteLine("Write 1 for Code, 2 for Name, 3 for Measurement unit or 4 for Location\n" +
                    "Press 5 to save\n");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Write the new code \n");
                        variableToUpdate.Variable.Code = Console.ReadLine();
                        break;

                    case "2":
                        Console.WriteLine("Write the new name \n");
                        variableToUpdate.Variable.VariableType.Name = Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine("Write the new measuremnt unit \n");
                        variableToUpdate.Variable.VariableType.MeasurementUnit = Console.ReadLine();
                        break;

                    case "4":
                        Console.WriteLine("Select the new location type: \n" +
                            "1 - Building \n" +
                            "2 - Floor \n" +
                            "3 - Room \n");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                Console.WriteLine("Select the corresponding building \n");
                                
                                var allBuildings = GetAllBuildings(channel);

                                var buildingLocation = buildingClient.GetBuilding(new GetRequest() { Id = allBuildings.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });

                                variableToUpdate.Variable.Building = buildingLocation.Building;
                                variableToUpdate.Variable.LocationId = buildingLocation.Building.Id;
                                variableToUpdate.Variable.StructureType = StructureType.Building;
                                break;

                            case "2":
                                Console.WriteLine("Select the corresponding floor \n");
                                var allFloors = GetAllFloors(channel);

                                var floorLocation = floorClient.GetFloor(new GetRequest() { Id = allFloors.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
                                var buildingOfFloor = buildingClient.GetBuilding(new GetRequest() { Id = floorLocation.Floor.BuildingId });

                                variableToUpdate.Variable.Floor = floorLocation.Floor;                                
                                variableToUpdate.Variable.LocationId = floorLocation.Floor.Id;
                                variableToUpdate.Variable.StructureType = StructureType.Floor;
                                variableToUpdate.Variable.Floor.Building = buildingOfFloor.Building;
                                break;

                            case "3":
                                Console.WriteLine("Select the corresponding room: \n");
                                var roomClient = new Room.RoomClient(channel);
                                var allRooms = GetAllRooms(channel);

                                var roomLocation = roomClient.GetRoom(new GetRequest() { Id = allRooms.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
                                var floorOfRoom = floorClient.GetFloor(new GetRequest() { Id = roomLocation.Room.FloorId });
                                var buildingOfFloor2 = buildingClient.GetBuilding(new GetRequest() { Id = floorOfRoom.Floor.BuildingId });
                                variableToUpdate.Variable.Room = roomLocation.Room;
                                variableToUpdate.Variable.LocationId = roomLocation.Room.Id;
                                variableToUpdate.Variable.StructureType = StructureType.Room;
                                variableToUpdate.Variable.Floor = floorOfRoom.Floor;
                                variableToUpdate.Variable.Floor.Building = buildingOfFloor2.Building;
                                break;
                        }
                        break;

                    case "5":
                        loop = false;
                        break;

                    default:
                        Console.WriteLine("Invalid action");
                        break;
                }
            }


            variableClient.UpdateVariable(variableToUpdate.Variable);

            var updatedGetResponse = variableClient.GetVariable(new GetRequest() { Id = variableToUpdate.Variable.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableVariableDTO.KindOneofCase.Variable &&
                updatedGetResponse.Variable.Code == variableToUpdate.Variable.Code &&
                updatedGetResponse.Variable.VariableType == variableToUpdate.Variable.VariableType &&
                updatedGetResponse.Variable.LocationCase == variableToUpdate.Variable.LocationCase)
            {
                Console.WriteLine($"Updated succesfully.\n");
            }
        }

        public static void UpdateSampleInt(GrpcChannel channel)
        {
            var sampleClient = new SampleInt.SampleIntClient(channel);
            var variableClient = new Variable.VariableClient(channel);

            Console.WriteLine("Select the sample you want to modify?: \n");
            var allSamples = GetAllSampleInts(channel);
            SampleIntDTO? sampleToUpdate = allSamples.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            if (sampleToUpdate is null)
            {
                Console.WriteLine("Invalid input");
                return;
            }

            bool loop = true;
            VariableDTO variable = variableClient.GetVariable(new GetRequest() { Id = sampleToUpdate.VariableId }).Variable;
            while (loop)
            {
                Console.WriteLine("What do you want to modify? \n" +
                "Variable: " + variable.Code + "\n" +
                "Date & Time: " + sampleToUpdate.DateTime + "\n" +
                "Value: " + sampleToUpdate.Value.ToString() + "\n");

                Console.WriteLine("Write 1 for Variable, 2 for Date & Time or 3 for Value\n" +
                    "Press 4 to save");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Select the new Variable\n");
                        var allVariables = GetAllVariables(channel);
                        sampleToUpdate.VariableId = allVariables.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id;
                        break;
                    case "2":
                        Console.WriteLine("Insert the day (dd): ");
                        string dateTime = Console.ReadLine();
                        Console.WriteLine("Insert the month (mm): ");
                        dateTime = dateTime + "/" + Console.ReadLine();
                        Console.WriteLine("Insert the year (yyyy): ");
                        dateTime = dateTime + "/" + Console.ReadLine();
                        Console.WriteLine("Insert the hour (hh): ");
                        dateTime = dateTime + " " + Console.ReadLine();
                        Console.WriteLine("Insert the minutes (mm): ");
                        dateTime = dateTime + ":" + Console.ReadLine();
                        Console.WriteLine("Insert the seconds (ss): ");
                        dateTime = dateTime + ":" + Console.ReadLine();


                        try
                        {
                            DateTime format = System.DateTime.Parse(dateTime);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid format");
                            break;
                        }
                        sampleToUpdate.DateTime = dateTime;
                        break;

                    case "3":
                        Console.WriteLine("Insert the value: ");
                        sampleToUpdate.Value = Convert.ToInt32(Console.ReadLine());
                        break;
                    case "4":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }

            sampleClient.UpdateSampleInt(sampleToUpdate);

            var sampleGetResponse = sampleClient.GetSampleIntByTimeSpan(
                new GrpcProtos.TimeSpan() { StartTime = sampleToUpdate.DateTime, EndTime = sampleToUpdate.DateTime });
            foreach (SampleIntDTO item in sampleGetResponse.Items)
            {
                if (item.VariableId == sampleToUpdate.VariableId)
                    if (item.Value == sampleToUpdate.Value)
                        Console.WriteLine($"Updated succesfully.\n");
            }
        }

        public static void UpdateSampleDouble(GrpcChannel channel)
        {
            var sampleClient = new SampleDouble.SampleDoubleClient(channel);
            var variableClient = new Variable.VariableClient(channel);

            Console.WriteLine("Select the sample you want to modify?: \n");
            var allSamples = GetAllSampleDoubles(channel);
            SampleDoubleDTO? sampleToUpdate = allSamples.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            if (sampleToUpdate is null)
            {
                Console.WriteLine("Invalid input");
                return;
            }

            bool loop = true;
            VariableDTO variable = variableClient.GetVariable(new GetRequest() { Id = sampleToUpdate.VariableId }).Variable;
            while (loop)
            {
                Console.WriteLine("What do you want to modify? \n" +
                "Variable: " + variable.Code + "\n" +
                "Date & Time: " + sampleToUpdate.DateTime + "\n" +
                "Value: " + sampleToUpdate.Value.ToString() + "\n");

                Console.WriteLine("Write 1 for Variable, 2 for Date & Time or 3 for Value\n" +
                    "Press 4 to save");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Select the new Variable\n");
                        var allVariables = GetAllVariables(channel);
                        sampleToUpdate.VariableId = allVariables.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id;
                        break;
                    case "2":
                        Console.WriteLine("Insert the day (dd): ");
                        string dateTime = Console.ReadLine();
                        Console.WriteLine("Insert the month (mm): ");
                        dateTime = dateTime + "/" + Console.ReadLine();
                        Console.WriteLine("Insert the year (yyyy): ");
                        dateTime = dateTime + "/" + Console.ReadLine();
                        Console.WriteLine("Insert the hour (hh): ");
                        dateTime = dateTime + " " + Console.ReadLine();
                        Console.WriteLine("Insert the minutes (mm): ");
                        dateTime = dateTime + ":" + Console.ReadLine();
                        Console.WriteLine("Insert the seconds (ss): ");
                        dateTime = dateTime + ":" + Console.ReadLine();


                        try
                        {
                            DateTime format = System.DateTime.Parse(dateTime);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid format");
                            break;
                        }
                        sampleToUpdate.DateTime = dateTime;
                        break;

                    case "3":
                        Console.WriteLine("Insert the value: ");
                        sampleToUpdate.Value = double.Parse(Console.ReadLine());
                        break;
                    case "4":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }

            sampleClient.UpdateSampleDouble(sampleToUpdate);

            var sampleGetResponse = sampleClient.GetSampleDoubleByTimeSpan(
                new GrpcProtos.TimeSpan() { StartTime = sampleToUpdate.DateTime, EndTime = sampleToUpdate.DateTime });
            foreach (SampleDoubleDTO item in sampleGetResponse.Items)
            {
                if (item.VariableId == sampleToUpdate.VariableId)
                    if (item.Value == sampleToUpdate.Value)
                        Console.WriteLine($"Updated succesfully.\n");
            }
        }

        public static void UpdateSampleBool(GrpcChannel channel)
        {
            var sampleClient = new SampleBool.SampleBoolClient(channel);
            var variableClient = new Variable.VariableClient(channel);

            Console.WriteLine("Select the sample you want to modify?: \n");
            var allSamples = GetAllSampleBools(channel);
            SampleBoolDTO? sampleToUpdate = allSamples.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            if (sampleToUpdate is null)
            {
                Console.WriteLine("Invalid input");
                return;
            }

            bool loop = true;
            VariableDTO variable = variableClient.GetVariable(new GetRequest() { Id = sampleToUpdate.VariableId }).Variable;
            while (loop)
            {
                Console.WriteLine("What do you want to modify? \n" +
                "Variable: " + variable.Code + "\n" +
                "Date & Time: " + sampleToUpdate.DateTime + "\n" +
                "Value: " + sampleToUpdate.Value.ToString() + "\n");

                Console.WriteLine("Write 1 for Variable, 2 for Date & Time or 3 for Value\n" +
                    "Press 4 to save");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Select the new Variable\n");
                        var allVariables = GetAllVariables(channel);
                        sampleToUpdate.VariableId = allVariables.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id;
                        break;
                    case "2":
                        Console.WriteLine("Insert the day (dd): ");
                        string dateTime = Console.ReadLine();
                        Console.WriteLine("Insert the month (mm): ");
                        dateTime = dateTime + "/" + Console.ReadLine();
                        Console.WriteLine("Insert the year (yyyy): ");
                        dateTime = dateTime + "/" + Console.ReadLine();
                        Console.WriteLine("Insert the hour (hh): ");
                        dateTime = dateTime + " " + Console.ReadLine();
                        Console.WriteLine("Insert the minutes (mm): ");
                        dateTime = dateTime + ":" + Console.ReadLine();
                        Console.WriteLine("Insert the seconds (ss): ");
                        dateTime = dateTime + ":" + Console.ReadLine();


                        try
                        {
                            DateTime format = System.DateTime.Parse(dateTime);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid format");
                            break;
                        }
                        sampleToUpdate.DateTime = dateTime;
                        break;

                    case "3":
                        Console.WriteLine("Insert the value: ");
                        sampleToUpdate.Value = Convert.ToBoolean(Console.ReadLine());
                        break;
                    case "4":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }

            sampleClient.UpdateSampleBool(sampleToUpdate);

            var sampleGetResponse = sampleClient.GetSampleBoolByTimeSpan(
                new GrpcProtos.TimeSpan() { StartTime = sampleToUpdate.DateTime, EndTime = sampleToUpdate.DateTime });
            foreach (SampleBoolDTO item in sampleGetResponse.Items)
            {
                if (item.VariableId == sampleToUpdate.VariableId)
                    if (item.Value == sampleToUpdate.Value)
                        Console.WriteLine($"Updated succesfully.\n");
            }

        }

        public static void DeleteBuilding(GrpcChannel channel)
        {
            var buildingClient = new Building.BuildingClient(channel);
            Console.WriteLine("Select the corresponding building \n");
            var allBuildings = GetAllBuildings(channel);

            var buildingToDelete = buildingClient.GetBuilding(new GetRequest() { Id = allBuildings.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
            buildingClient.DeleteBuilding(new DeleteRequest() { Id = buildingToDelete.Building.Id });
            var deletedGetResponse = buildingClient.GetBuilding(new GetRequest() { Id = buildingToDelete.Building.Id });
            if (deletedGetResponse is null ||
                deletedGetResponse.KindCase != NullableBuildingDTO.KindOneofCase.Building)
            {
                Console.WriteLine($"Eliminación exitosa.");
            }
        }

        public static void DeleteFloor(GrpcChannel channel)
        {
            var floorClient = new Floor.FloorClient(channel);
            Console.WriteLine("Select the corresponding floor \n");
            var allFloors = GetAllFloors(channel);

            var floorToDelete = floorClient.GetFloor(new GetRequest() { Id = allFloors.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
            floorClient.DeleteFloor(new DeleteRequest() { Id = floorToDelete.Floor.Id });
            var deletedGetResponse = floorClient.GetFloor(new GetRequest() { Id = floorToDelete.Floor.Id });
            if (deletedGetResponse is null ||
                deletedGetResponse.KindCase != NullableFloorDTO.KindOneofCase.Floor)
            {
                Console.WriteLine($"Eliminación exitosa.");
            }
        }

        public static void DeleteRoom(GrpcChannel channel)
        {
            var roomClient = new Room.RoomClient(channel);
            Console.WriteLine("Select the corresponding room: \n");
            var allRooms = GetAllRooms(channel);

            var roomToDelete = roomClient.GetRoom(new GetRequest() { Id = allRooms.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
            roomClient.DeleteRoom(new DeleteRequest() { Id = roomToDelete.Room.Id });
            var deletedGetResponse = roomClient.GetRoom(new GetRequest() { Id = roomToDelete.Room.Id });
            if (deletedGetResponse is null ||
                deletedGetResponse.KindCase != NullableRoomDTO.KindOneofCase.Room)
            {
                Console.WriteLine($"Eliminación exitosa.");
            }
        }

        public static void DeleteVariable(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            Console.WriteLine("Select the corresponding variable: \n");
            var allVariables = GetAllVariables(channel);

            var variableToDelete = variableClient.GetVariable(new GetRequest() { Id = allVariables.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
            variableClient.DeleteVariable(new DeleteRequest() { Id = variableToDelete.Variable.Id });
            var deletedGetResponse = variableClient.GetVariable(new GetRequest() { Id = variableToDelete.Variable.Id });
            if (deletedGetResponse is null ||
                deletedGetResponse.KindCase != NullableVariableDTO.KindOneofCase.Variable)
            {
                Console.WriteLine($"Deleted successfully");
            }
        }

        public static void DeleteSampleInt(GrpcChannel channel)
        {
            var sampleClient = new SampleInt.SampleIntClient(channel);
            Console.WriteLine("Select the corresponding Sample");
            var allSamples = GetAllSampleInts(channel);
            var sampleToDelete = allSamples.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            sampleClient.DeleteSampleInt(new DeleteSampleRequest() { VariableId = sampleToDelete.VariableId, DateTime = sampleToDelete.DateTime });

            var deletedGetResponse = sampleClient.GetSampleIntByTimeSpan(
               new GrpcProtos.TimeSpan() { StartTime = sampleToDelete.DateTime, EndTime = sampleToDelete.DateTime });

            int count = 0;
            foreach (SampleIntDTO item in deletedGetResponse.Items)
            {
                if (item.VariableId == sampleToDelete.VariableId)
                    count++;
            }
            if (count == 0)
                Console.WriteLine($"Deleted successfully");
        }

        public static void DeleteSampleDouble(GrpcChannel channel)
        {
            var sampleClient = new SampleDouble.SampleDoubleClient(channel);
            Console.WriteLine("Select the corresponding Sample");
            var allSamples = GetAllSampleDoubles(channel);
            var sampleToDelete = allSamples.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            sampleClient.DeleteSampleDouble(new DeleteSampleRequest() { VariableId = sampleToDelete.VariableId, DateTime = sampleToDelete.DateTime });

            var deletedGetResponse = sampleClient.GetSampleDoubleByTimeSpan(
               new GrpcProtos.TimeSpan() { StartTime = sampleToDelete.DateTime, EndTime = sampleToDelete.DateTime });

            int count = 0;
            foreach (SampleDoubleDTO item in deletedGetResponse.Items)
            {
                if (item.VariableId == sampleToDelete.VariableId)
                    count++;
            }
            if (count == 0)
                Console.WriteLine($"Deleted successfully");
        }

        public static void DeleteSampleBool(GrpcChannel channel)
        {
            var sampleClient = new SampleBool.SampleBoolClient(channel);
            Console.WriteLine("Select the corresponding Sample");
            var allSamples = GetAllSampleBools(channel);
            var sampleToDelete = allSamples.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            sampleClient.DeleteSampleBool(new DeleteSampleRequest() { VariableId = sampleToDelete.VariableId, DateTime = sampleToDelete.DateTime });

            var deletedGetResponse = sampleClient.GetSampleBoolByTimeSpan(
               new GrpcProtos.TimeSpan() { StartTime = sampleToDelete.DateTime, EndTime = sampleToDelete.DateTime });

            int count = 0;
            foreach (SampleBoolDTO item in deletedGetResponse.Items)
            {
                if (item.VariableId == sampleToDelete.VariableId)
                    count++;
            }
            if (count == 0)
                Console.WriteLine($"Deleted successfully");
        }

        public static Buildings? GetAllBuildings(GrpcChannel channel)
        {
            var buildingClient = new Building.BuildingClient(channel);
            var getResponse = buildingClient.GetAllBuildings(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    Console.WriteLine(i + " - Number: " + getResponse.Items[i - 1].Number + "\n\t" +
                        "Address: " + getResponse.Items[i - 1].Address + "\n");
                }
            }
            return getResponse;
        }

        public static Floors? GetAllFloors(GrpcChannel channel)
        {
            var floorClient = new Floor.FloorClient(channel);
            var buildingClient = new Building.BuildingClient(channel);
            var getResponse = floorClient.GetAllFloors(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var building = buildingClient.GetBuilding(new GetRequest() { Id = getResponse.Items[i - 1].BuildingId });
                    Console.WriteLine(i + " - Location: " + getResponse.Items[i - 1].Location + "\n\t" +
                        "Building Number: " + building.Building.Number + "\n\t" +
                        "Building Address: " + building.Building.Address + "\n");
                };
            }
            return getResponse;
        }

        public static Rooms? GetAllRooms(GrpcChannel channel)
        {
            var roomClient = new Room.RoomClient(channel);
            var getResponse = roomClient.GetAllRooms(new Google.Protobuf.WellKnownTypes.Empty());
            var floorClient = new Floor.FloorClient(channel);
            var buildingClient = new Building.BuildingClient(channel);

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var floorOfRoom = floorClient.GetFloor(new GetRequest() { Id = getResponse.Items[i - 1].FloorId });
                    var buildingOfFloor = buildingClient.GetBuilding(new GetRequest() { Id = floorOfRoom.Floor.BuildingId });

                    
                    if (getResponse.Items[i - 1].IsProduction is true)
                    {
                        Console.WriteLine(i + " - Number: " + getResponse.Items[i - 1].Number + "\n\t" +
                        "Description: " + getResponse.Items[i - 1].Description + "\n\t" +
                        "Floor Location: " + floorOfRoom.Floor.Location + "\n\t" +
                        "Building Number: " + buildingOfFloor.Building.Number + "\n\t" +
                        "Building Address: " + buildingOfFloor.Building.Address + "\n\t" +
                        "Type: Production \n");
                    }
                    else
                    {
                        Console.WriteLine(i + " - Number: " + getResponse.Items[i - 1].Number + "\n\t" +
                        "Description: " + getResponse.Items[i - 1].Description + "\n\t" +
                        "Floor Location: " + floorOfRoom.Floor.Location + "\n\t" +
                        "Building Number: " + buildingOfFloor.Building.Number + "\n\t" +
                        "Building Address: " + buildingOfFloor.Building.Address + "\n\t" +
                        "Type: Office \n");
                    }
                }
            }
            return getResponse;
        }

        public static Variables? GetAllVariables(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var buildingClient = new Building.BuildingClient(channel);
            var floorClient = new Floor.FloorClient(channel);
            var roomClient = new Room.RoomClient(channel);

            var getResponse = variableClient.GetAllVariables(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                                        
                    if (getResponse.Items[i - 1].StructureType == StructureType.Building)
                    {
                        var buildingOfVariable = buildingClient.GetBuilding(new GetRequest() { Id = getResponse.Items[i - 1].LocationId});
                        Console.WriteLine(i + " - Code: " + getResponse.Items[i - 1].Code + "\n\t" +
                        "Name: " + getResponse.Items[i - 1].VariableType.Name + "\n\t" +
                        "Measurement unit: " + getResponse.Items[i - 1].VariableType.MeasurementUnit + "\n\t" + 
                        "Location: Building No." + buildingOfVariable.Building.Number.ToString() + "\n");
                    }
                    else if (getResponse.Items[i - 1].StructureType == StructureType.Floor)
                    {
                        var floorOfVariable = floorClient.GetFloor(new GetRequest() { Id = getResponse.Items[i - 1].LocationId });
                        var buildingOfFloor = buildingClient.GetBuilding(new GetRequest() { Id = floorOfVariable.Floor.BuildingId });
                        var buildingOfVariable = buildingClient.GetBuilding(new GetRequest() { Id = getResponse.Items[i - 1].LocationId });
                        Console.WriteLine(i + " - Code: " + getResponse.Items[i - 1].Code + "\n\t" +
                        "Name: " + getResponse.Items[i - 1].VariableType.Name + "\n\t" +
                        "Measurement unit: " + getResponse.Items[i - 1].VariableType.MeasurementUnit + "\n\t" + 
                        "Location: " + floorOfVariable.Floor.Location + " of Building No." + buildingOfFloor.Building.Number.ToString() + "\n");
                    }
                    else 
                    {
                        var roomOfVariable = roomClient.GetRoom(new GetRequest() { Id = getResponse.Items[i - 1].LocationId });
                        var floorOfRoom = floorClient.GetFloor(new GetRequest() { Id = roomOfVariable.Room.FloorId});
                        var buildingOfFloor = buildingClient.GetBuilding(new GetRequest() { Id = floorOfRoom.Floor.BuildingId });
                        Console.WriteLine(i + " - Code: " + getResponse.Items[i - 1].Code + "\n\t" +
                        "Name: " + getResponse.Items[i - 1].VariableType.Name + "\n\t" +
                        "Measurement unit: " + getResponse.Items[i - 1].VariableType.MeasurementUnit + "\n\t" + 
                        "Location: Room No." + roomOfVariable.Room.Number.ToString() + " of " + floorOfRoom.Floor.Location +
                        " of Building No." + buildingOfFloor.Building.Number.ToString() + "\n");
                    }
                };
            }
            return getResponse;
        }

        public static SampleInts? GetAllSampleInts(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleInt.SampleIntClient(channel);
            var getResponse = sampleClient.GetAllSampleInts(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }

        public static SampleDoubles? GetAllSampleDoubles(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleDouble.SampleDoubleClient(channel);
            var getResponse = sampleClient.GetAllSampleDoubles(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }

        public static SampleBools? GetAllSampleBools(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleBool.SampleBoolClient(channel);
            var getResponse = sampleClient.GetAllSampleBools(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }

        public static NullableBuildingDTO GetBuilding(GrpcChannel channel)
        {
            var buildingClient = new Building.BuildingClient(channel);
            var floorClient = new Floor.FloorClient(channel);
            var variableClient = new Variable.VariableClient(channel);

            Console.WriteLine("Select the Building: \n");
            var allBuildings = GetAllBuildings(channel);

            var getResponse = buildingClient.GetBuilding(new GetRequest() { Id = allBuildings.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
            Console.WriteLine("SELECTED Building No." + getResponse.Building.Number.ToString() + " Address: " + getResponse.Building.Address + "\n");

            var allFloors = floorClient.GetAllFloors(new Google.Protobuf.WellKnownTypes.Empty());
            Console.WriteLine("\nFloors List: \n");
            int i = 1;
            foreach(FloorDTO floor in allFloors.Items)
            {
                if (floor.BuildingId == getResponse.Building.Id)
                {
                    Console.WriteLine(i + " - Location: " + floor.Location + "\n");
                    i++;
                }
            }
            var allVariables = variableClient.GetAllVariables(new Google.Protobuf.WellKnownTypes.Empty());
            i = 1;
            Console.WriteLine("\nVariables List: \n");
            foreach (VariableDTO variable in allVariables.Items)
            {
                if (variable.LocationId == getResponse.Building.Id)
                {
                    Console.WriteLine(i + " - Code: " + variable.Code + "\n\t" +
                        "Name: " + variable.VariableType.Name + "\n\t" +
                        "Measurement unit: " + variable.VariableType.MeasurementUnit + "\n");
                    i++;
                }
            }
            return getResponse;
        }

        public static NullableFloorDTO GetFloor(GrpcChannel channel)
        {
            var roomClient = new Room.RoomClient(channel);
            var floorClient = new Floor.FloorClient(channel);
            var variableClient = new Variable.VariableClient(channel);

            Console.WriteLine("Select the Floor: \n");
            var allFloors = GetAllFloors(channel);

            var getResponse = floorClient.GetFloor(new GetRequest() { Id = allFloors.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
            Console.WriteLine("SELECTED Floor " + getResponse.Floor.Location + "\n");

            var allRooms = roomClient.GetAllRooms(new Google.Protobuf.WellKnownTypes.Empty());
            Console.WriteLine("\nRooms List: \n");
            int i = 1;
            foreach (RoomDTO room in allRooms.Items)
            {
                if (room.FloorId == getResponse.Floor.Id)
                {
                    Console.WriteLine(i + " - Number: " + room.Number.ToString() + "\n\t" +
                        "Description: " + room.Description + "\n");
                    i++;
                }
            }
            var allVariables = variableClient.GetAllVariables(new Google.Protobuf.WellKnownTypes.Empty());
            i = 1;
            Console.WriteLine("\nVariables List: \n");
            foreach (VariableDTO variable in allVariables.Items)
            {
                if (variable.LocationId == getResponse.Floor.Id)
                {
                    Console.WriteLine(i + " - Code: " + variable.Code + "\n\t" +
                        "Name: " + variable.VariableType.Name + "\n\t" +
                        "Measurement unit: " + variable.VariableType.MeasurementUnit + "\n");
                    i++;
                }
            }
            return getResponse;
        }
    
        public static NullableRoomDTO GetRoom(GrpcChannel channel)
        {
            var roomClient = new Room.RoomClient(channel);
            var variableClient = new Variable.VariableClient(channel);

            Console.WriteLine("Select the Room: \n");
            var allRooms = GetAllRooms(channel);

            var getResponse = roomClient.GetRoom(new GetRequest() { Id = allRooms.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
            Console.WriteLine("SELECTED Room No." + getResponse.Room.Number.ToString() + " Description: " + getResponse.Room.Description + "\n");

            
            var allVariables = variableClient.GetAllVariables(new Google.Protobuf.WellKnownTypes.Empty());
            int i = 1;
            Console.WriteLine("\nVariables List: \n");
            foreach (VariableDTO variable in allVariables.Items)
            {
                if (variable.LocationId == getResponse.Room.Id)
                {
                    Console.WriteLine(i + " - Code: " + variable.Code + "\n\t" +
                        "Name: " + variable.VariableType.Name + "\n\t" +
                        "Measurement unit: " + variable.VariableType.MeasurementUnit + "\n");
                    i++;
                }
            }
            return getResponse;
        }

        public static NullableVariableDTO GetVariable(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleIntClient = new SampleInt.SampleIntClient(channel);
            var sampleDoubleClient = new SampleDouble.SampleDoubleClient(channel);
            var sampleBoolClient = new SampleBool.SampleBoolClient(channel);

            Console.WriteLine("Select the Variable: \n");
            var allVariables = GetAllVariables(channel);

            var getResponse = variableClient.GetVariable(new GetRequest() { Id = allVariables.Items[Convert.ToInt32(Console.ReadLine()) - 1].Id });
            Console.WriteLine("SELECTED Variable Code: " + getResponse.Variable.Code + " Name: " + getResponse.Variable.VariableType.Name + " MeasurementUnit: " + getResponse.Variable.VariableType.MeasurementUnit + "\n");

            int i = 1;
            Console.WriteLine("\nSamples List: \n");
            var allSampleInts = sampleIntClient.GetAllSampleInts(new Google.Protobuf.WellKnownTypes.Empty());
            foreach (SampleIntDTO sample in allSampleInts.Items)
            {
                if (sample.VariableId == getResponse.Variable.Id)
                {
                    Console.WriteLine(i + " - Date&Time: " + sample.DateTime + "\t" +
                        "Value: " + sample.Value.ToString() + "\n");
                    i++;
                }
            }
            var allSampleDoubles = sampleDoubleClient.GetAllSampleDoubles(new Google.Protobuf.WellKnownTypes.Empty());
            foreach (SampleDoubleDTO sample in allSampleDoubles.Items)
            {
                if (sample.VariableId == getResponse.Variable.Id)
                {
                    Console.WriteLine(i + " - Date&Time: " + sample.DateTime + "\t" +
                        "Value: " + sample.Value.ToString() + "\n");
                    i++;
                }
            }
            var allSampleBools = sampleBoolClient.GetAllSampleBools(new Google.Protobuf.WellKnownTypes.Empty());
            foreach (SampleBoolDTO sample in allSampleBools.Items)
            {
                if (sample.VariableId == getResponse.Variable.Id)
                {
                    Console.WriteLine(i + " - Date&Time: " + sample.DateTime + "\t" +
                        "Value: " + sample.Value.ToString() + "\n");
                    i++;
                }
            }
            return getResponse;
        }

        public static NullableSampleIntDTO GetSampleInt(GrpcChannel channel, SampleInts allSampleInts)
        {
            var variableClient = new Variable.VariableClient(channel);
            VariableDTO variable = variableClient.GetVariable(new GetRequest() { Id = allSampleInts.Items[Convert.ToInt32(Console.ReadLine()) - 1].VariableId }).Variable;
            Console.WriteLine("Select the corresponding sample. \n");
            Console.WriteLine("SELECTED SAMPLE \n" +
            "Variable: " + variable.Code + "\n" +
            "Date & Time: " + allSampleInts.Items[Convert.ToInt32(Console.ReadLine()) - 1].DateTime + "\n" +
            "Value: " + allSampleInts.Items[Convert.ToInt32(Console.ReadLine()) - 1].Value.ToString() + "\n");
            var getResponse = allSampleInts.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            return getResponse;
        }

        public static NullableSampleDoubleDTO GetSampleDouble(GrpcChannel channel, SampleDoubles allSampleDoubles)
        {
            var variableClient = new Variable.VariableClient(channel);
            VariableDTO variable = variableClient.GetVariable(new GetRequest() { Id = allSampleDoubles.Items[Convert.ToInt32(Console.ReadLine()) - 1].VariableId }).Variable;
            Console.WriteLine("Select the corresponding sample. \n");
            Console.WriteLine("SELECTED SAMPLE \n" +
            "Variable: " + variable.Code + "\n" +
            "Date & Time: " + allSampleDoubles.Items[Convert.ToInt32(Console.ReadLine()) - 1].DateTime + "\n" +
            "Value: " + allSampleDoubles.Items[Convert.ToInt32(Console.ReadLine()) - 1].Value.ToString() + "\n");
            var getResponse = allSampleDoubles.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            return getResponse;
        }

        public static NullableSampleBoolDTO GetSampleBool(GrpcChannel channel, SampleBools allSampleBools)
        {
            var variableClient = new Variable.VariableClient(channel);
            VariableDTO variable = variableClient.GetVariable(new GetRequest() { Id = allSampleBools.Items[Convert.ToInt32(Console.ReadLine()) - 1].VariableId }).Variable;
            Console.WriteLine("Select the corresponding sample. \n");
            Console.WriteLine("SELECTED SAMPLE \n" +
            "Variable: " + variable.Code + "\n" +
            "Date & Time: " + allSampleBools.Items[Convert.ToInt32(Console.ReadLine()) - 1].DateTime + "\n" +
            "Value: " + allSampleBools.Items[Convert.ToInt32(Console.ReadLine()) - 1].Value.ToString() + "\n");
            var getResponse = allSampleBools.Items[Convert.ToInt32(Console.ReadLine()) - 1];
            return getResponse;
        }

        public static SampleInts? GetSampleIntByVariableId(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleInt.SampleIntClient(channel);
            var getResponse = sampleClient.GetAllSampleInts(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }

        public static SampleDoubles? GetSampleDoubleByVariableId(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleDouble.SampleDoubleClient(channel);
            var getResponse = sampleClient.GetAllSampleDoubles(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }

        public static SampleBools? GetSampleBoolByVariableId(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleBool.SampleBoolClient(channel);
            var getResponse = sampleClient.GetAllSampleBools(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }

        public static SampleInts? GetSampleIntByTimeSpan(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleInt.SampleIntClient(channel);
            var getResponse = sampleClient.GetAllSampleInts(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }

        public static SampleDoubles? GetSampleDoubleByTimeSpan(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleDouble.SampleDoubleClient(channel);
            var getResponse = sampleClient.GetAllSampleDoubles(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }

        public static SampleBools? GetSampleBoolByTimeSpan(GrpcChannel channel)
        {
            var variableClient = new Variable.VariableClient(channel);
            var sampleClient = new SampleBool.SampleBoolClient(channel);
            var getResponse = sampleClient.GetAllSampleBools(new Google.Protobuf.WellKnownTypes.Empty());

            if (getResponse.Items is null)
            {
                Console.WriteLine("There is none");
                channel.Dispose();
            }
            else
            {
                for (int i = 1; i <= getResponse.Items.Count; i++)
                {
                    var variable = variableClient.GetVariable(new GetRequest() { Id = getResponse.Items[i - 1].VariableId });

                    Console.WriteLine(i + " - Variable Code: " + variable.Variable.Code + "\n\t" +
                        "Date&Time: " + getResponse.Items[i - 1].DateTime + "\n\t" +
                        "Value: " + getResponse.Items[i - 1].Value.ToString() + "\n");
                };
            }
            return getResponse;
        }
    }
}











