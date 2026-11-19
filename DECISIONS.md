# DECISIONS

Answer **yourself**, in your own words, in 3–5 sentences per question.
Refer to concrete files and classes from your own repo (for example `Features/Bookings/CreateBooking/...`).
An unanswered question gives 0 points.

## 1. A rule you moved
Pick one business rule (R1–R8). In which file did it live in the starter and in which file does it live now? Why there?

Am mutat regula R7. Era in Services/BookingService.cs, in metoda Create.
Acum e în Domain/Rooms/Room.cs in Room.Book care foloseste Booking.Overlaps.
Am mutat-o acolo ca regula sa stea langa datele pe care le protejeaza.

## 2. Result instead of an exception
Why is `Result` a better choice than an exception for "the room is occupied"? What happens in your code when an `Error` is returned?

Camera ocupata e un caz normal de buisiness, deci e un resultat la un booking esuat, dar nu o eroare. Cand se intoarce Error e propagat mai departe, handler-ul il returnaza mai departe pana se ajunge la o eroare HTTP de tip 4xx nu ca Middleware.

## 3. Who knows about EF Core?
Which classes in your project know about EF Core and which do not? What do you gain and what do you pay with Repository + Unit of Work?

EF Core il stiu doar clasele din Infrastructure/Persistence: RoomHubDbContext, RoomConfiguration, BookingConfiguration, RoomRepository, BookingRepository, UnitOfWork și UtcDateTimeConverter.

## 4. The flow of a request
Draw in text (ASCII arrows) the path of a `POST /api/rooms/{roomId}/bookings` request through your project, with the **real** names of files and classes, from the endpoint to the database.

Nu am timp dar incerc sa fac ceva de baza
POST /api/rooms/{roomId}/bookings
  -> CreateBookingEndpoint (MapCreateBooking)
  -> CreateBookingCommand
  -> CreateBookingHandler
    ->...
  -> RoomHubDbContext -> SQLite
  -> Results.Created (201)  sau  Error.ToProblem (400/404/409)

## 5. What you rejected from what the AI gave you
Give a concrete example of code or an idea proposed by the AI that you corrected or rejected, and say why.

AI-ul a trimis pachetul EF core 11.0.0 pentru .net 11 si a crapat. Am schimabt eu sa fie o versiune compatibila.
Pentru R9 in loc sa bag toate rezervarile in memorie am facut cu CountAsync.

## 6. If you had 30 more minutes
What would you improve and what risk did you leave in the project?

In primul rand as face unit testing si as incerca sa ating code coverage cat mai mare.
Dupa as face un CI ca sa pot da push la cod fara stress.
EnsureCreated in loc de migrari.