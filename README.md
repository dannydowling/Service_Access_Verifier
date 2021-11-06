# Service_Access_Verifier
In the registry, in the services hive, there is a type definition for each service.
The value of this determines what access the service has. Whether it's network capable or local only etc.

What I figured is that changing the type code could allow an attack, if say, local services ran as network available services.
So I made a thing that parses the registry keys and reads and writes from a file.

TODO:
This could probably use protocol buffers to serialize the data.
