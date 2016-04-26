Instrucciones
=============

### Connections.cs
Explicar brévemente cómo funciona la clase Connections.cs. Para conectarse con una base de datos las siguientes dos líneas se requieren siempre:

	Connection connection = new Connection("IP", Puerto); // El puerto normalmente es 3306
	connection.Open("Nombre base de datos", "usuario", "contraseña");

Leer filas de una base de datos:
	List<string> lista = connection.pull("tabla", "condición de fila"); 
la condición de fila es como sigue _columna='valor del campo'_, el método devuelve la fila cuyo campo de la columna indicada sea igual al valor indicado. El método se lee: *devuelve la fila donde en la columna _[columna]_ exita el valor _[valor campo]_*

Leer columnas de la base de datos:
	
	List<string> lista = connection.pullRow("tabla", "nombre de la columna"); 

devuelve los valores de la columna indicada

Actualizar campo de la base de datos
	
	connection.update("tabla", "condición de fila", "columna='valor nuevo'");

en la tabla dada y en la fila que cumpla con la condición dada, del tipo _columna='valor campo'_ actualizar el campo de la columna indicada por el nuevo valor.  

Escribir nueva fila a la base de datos. Primero se crea un diccionario, que es como una matriz donde indicas el nombre de las columnas y sus valores:

	Dictionary<string, string> dict = new Dictionary<string, string>();
	dict.Add("columna_1", "valor_1");
	// .
	// .
	// .
	dict.Add("columna_n", "valor_n");
	connection.push("tabla", dict); 
escribe en la tabla tal, la fila de valores que indica el diccionario.
