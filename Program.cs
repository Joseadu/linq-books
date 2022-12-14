using books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


LinqQueries queries = new LinqQueries();

// Toda la colección
// PrintValues(queries.AllCollection());

// Libros después del año 2000
// PrintValues(queries.BooksAfter2000());

// Libros +250 páginas y que contenga en el título 'in Action'
// PrintValues(queries.PagesAndAction());

// Cada uno de los libros tienen Status
// System.Console.WriteLine($"Todos los libros tienen status: {queries.HasStatusValue()}");

// Alguno publicado en 2005?
// System.Console.WriteLine($"Algún libro publicado en 2005?: {queries.HasAnyPublished2005()}");

// Libros con categoría Python
// PrintValues(queries.PythonCategory());

// Libros con categoría Java ordenados alfabeticametne
// PrintValues(queries.CatJavaOrderdByName());

// Libros con +450 páginas ordenados por página de forma descendente
// PrintValues(queries.Books450Desc());

// Libros con categoría Java seleccionando los 3 libros con publicación más reciente
// PrintValues(queries.JavaTake3Recent());

// Libros con +400 páginas omitiendo los 2 primeros
// PrintValues(queries.Book400Skip());

// Usando el operador Select seleccionar el título y el número de páginas de los 3 primeros libros
// PrintValues(queries.SelectFirst3Books());

// Usando el operador Count retornar el número de libros que tiene entre 200 y 500 páginas
// System.Console.WriteLine($"Cantidad de libros que tienen entre 200 y 500 páginas: {queries.CountBetween200And500()}");

// Usando el operador Min sacar el libro con menos fecha de publicación
// System.Console.WriteLine($"Libro con menor fecha de publicación: {queries.MinPublishedDate()}");

// Páginas del libro con el mayor número de páginas
// System.Console.WriteLine($"Páginas del libro con el mayor número de páginas: {queries.MaxPagesBook()}");

// Páginas del libro con el menor número de páginas
// System.Console.WriteLine($"Libro con el menor número de páginas > 0: {queries.MinbyBook().Title} - {queries.MinbyBook().PageCount} páginas");

// Suma de las páginas de los libros que tienen entre 200 y 500 páginas
// System.Console.WriteLine($"Suma de páginas de los libros entre 200 y 500 páginas: {queries.SumBooksPages()}");

// Suma de las páginas de los libros que tienen entre 200 y 500 páginas
// System.Console.WriteLine($"Media de caracteres en los títulos de los libros: {queries.CharactersAverage()}");

// Retornar todos los libros que fueron publicados a partir del 2000, agrupados por año
// ImprimirGrupo(queries.BooksFrom2000PerYear());

// Retornar diccionario usando Lookup que permita consultar libros de acuerdo a la letra con la que empieza el título
// PrintDictionary(queries.Dictionary(), 'A');

// Hacer un Join de dos listas. En este caso hay que coger los libros comunes (dependiendo de alguna condición) de las dos listas y con Join ver cuáles hay en común
PrintValues(queries.JoinExpression());


void PrintValues(IEnumerable<Book> booksList)
{
    // System.Console.WriteLine("{0, -60} {1, 7} {2, 11}\n", "Título", "Número de páginas", "Fecha publicación");
    foreach (var book in booksList)
    {
        System.Console.WriteLine("{0, -60} {1, 9} {2, 11}", book.Title, book.PageCount, book.PublishedDate);
    }
}

void ImprimirGrupo(IEnumerable<IGrouping<int, Book>> ListaDeLibros)
{
    foreach (var grupo in ListaDeLibros)
    {
        System.Console.WriteLine("");
        System.Console.WriteLine($"Grupo: {grupo.Key}");
        System.Console.WriteLine("{0, -60} {1, 7} {2, 11}\n", "Título", "Número de páginas", "Fecha publicación");

        foreach (var item in grupo)
        {
            System.Console.WriteLine("{0, -60} {1, 9} {2, 11}", item.Title, item.PageCount, item.PublishedDate);
        }
    }
}

void PrintDictionary(ILookup<char, Book> booksList, char letter)
{
    System.Console.WriteLine("{0, -60} {1, 7} {2, 11}\n", "Título", "Número de páginas", "Fecha publicación");
    foreach (var item in booksList[letter])
    {
        System.Console.WriteLine("{0, -60} {1, 9} {2, 11}", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString);
    }
}