using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books
{
    public class LinqQueries
    {
        // Crear lista de libros la cual llenaremos con los datos que vienen de books.json
        private List<Book> booksCollection = new List<Book>();
        public LinqQueries()
        {
            using(StreamReader reader = new StreamReader("books.json"))
            {
                string json = reader.ReadToEnd();
                this.booksCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() {PropertyNameCaseInsensitive = true})!;
            }
        }



        // MÉTODOS PARA MANIPULAR LA LISTA DE LIBROS

        // Toda la colección
        public IEnumerable<Book> AllCollection()
        {
            return booksCollection;
        }

        // Libros después del año 2000
        public IEnumerable<Book> BooksAfter2000()
        {
            // Extension method
            //return booksCollection.Where(p => p.PublishedDate.Year > 2000);

            // Query expresion
            return from book
                   in booksCollection
                   where book.PublishedDate.Year > 2000
                   select book;
        }

        // Libros +250 páginas y que contenga en el título 'in Action'
        public IEnumerable<Book> PagesAndAction()
        {
            // return booksCollection.Where(libro => libro.PageCount > 250 && libro.Title.Contains("in Action"));

            return from book
                   in booksCollection
                   where book.PageCount > 200 && book.Title.Contains("in Action")
                   select book;
        }

        // All devuelve un boolean. Te dice si todos los objetos tienen una condición
        // Cada uno de los libros tienen Status
        public bool HasStatusValue()
        {
            return booksCollection.All(book => book.Status != string.Empty);
        }

        // Any devuelve un boolean. Te dice si hay un objeto que contenga una condición
        // Alguno publicado en 2005?
        public bool HasAnyPublished2005()
        {
            return booksCollection.Any(book => book.PublishedDate.Year == 2005);
        }

        // Contains filtra los objetos que contengan una condición
        // Libros con categoría Python
        public IEnumerable<Book> PythonCategory()
        {
            return from book
                   in booksCollection
                   where book.Categories.Contains("Python")
                   select book;
        }

        // Libros con categoría Java y ordenados alfabeticamente
        public IEnumerable<Book> CatJavaOrderdByName()
        {
            // return booksCollection.Where(book => book.Categories.Contains("Java")).OrderBy(book => book.Title);
            
            return from book
                   in booksCollection
                   where book.Categories.Contains("Java") orderby book.Title
                   select book;
        }
        
        // Orderby ordena los objetos dependiendo de alguna condición
        // Libros con +450 páginas ordenados por página de forma descendente
        public IEnumerable<Book> Books450Desc()
        {
            return booksCollection.Where(book => book.PageCount > 450).OrderByDescending(book => book.PageCount);
        }
        
        // Take selecciona una cantidad determinada de objetos, al contrario de Skip que los descarta
        // Libros con categoría Java seleccionando los 3 libros con publicación más reciente
        public IEnumerable<Book> JavaTake3Recent()
        {
            return booksCollection.Where(book => book.Categories.Contains("Java")).OrderByDescending(book => book.PublishedDate).Take(3);
        }
        
        // Libros con +400 páginas omitiendo los 2 primeros
        public IEnumerable<Book> Book400Skip()
        {
            return booksCollection.Where(book => book.PageCount > 400).Take(6).Skip(2);
        }
        
        // Usando el operador Select seleccionar el título y el número de páginas de los 3 primeros libros
        public IEnumerable<Book> SelectFirst3Books()
        {
            return booksCollection.Take(3).Select(book => new Book() {Title = book.Title, PageCount = book.PageCount});
        }

        // Count cuenta el número de objetos que cumplan alguna condición
        // Usando el operador Count retornar el número de libros que tiene entre 200 y 500 páginas
        public int CountBetween200And500()
        {
            // return booksCollection.Where(book => book.PageCount > 200 && book.PageCount < 500).Count();
            return booksCollection.Count(book => book.PageCount >= 200 && book.PageCount < 500);
        }
        
        // Min retorna un entero. El mínimo de algo dependiendo de alguna condición
        // Usando el operador Min sacar el libro con menos fecha de publicación
        public DateTime MinPublishedDate()
        {
            return booksCollection.Min(book => book.PublishedDate);
        }
        
        // Retorna un entero. El máximo de algo dependiendo de alguna condición
        // Páginas del libro con el mayor número de páginas
        public int MaxPagesBook()
        {
            // return booksCollection.Max(book => book.PageCount);
            return (from book
                    in booksCollection
                    select book.PageCount).Max();
        }

        // Con Minby podemos retornar un objeto entero que cumpla con alguna condición
        // Libro con menor número de páginas pero que sea mayor que 0
        public Book? MinbyBook()
        {
            return booksCollection.Where(book => book.PageCount > 0).MinBy(book => book.PageCount);
        }
        
        // Con Sum podemos sumar valores de distintos objetos dependiendo de alguna condición
        // Suma de las páginas de los libros que tienen entre 200 y 500 páginas
        public int SumBooksPages()
        {
            return booksCollection.Where(book => book.PageCount <= 500 && book.PageCount >= 0).Sum(book => book.PageCount);
        }
        
        // Con Average podemos crear la media de algo
        // Promedio de caracteres que tienen los títulos de la colección
        public double CharactersAverage()
        {
            return booksCollection.Average(book => book.Title.Length);
        }
        
        
        // GroupBy permite hacer agrupaciones de objetos dependiendo de alguna condición
        // Retornar todos los libros que fueron publicados a partir del 2000, agrupados por año
        public IEnumerable<IGrouping<int, Book>> BooksFrom2000PerYear()
        {
            return booksCollection.Where(book => book.PublishedDate.Year >= 2000).GroupBy(book => book.PublishedDate.Year);
        }

        // Retornar diccionario usando Lookup que permita consultar libros de acuerdo a la letra con la que empieza el título
        public ILookup<char, Book> Dictionary()
        {
            return booksCollection.ToLookup(book => book.Title[0], book => book);
        }

        // Hacer un Join de dos listas. En este caso hay que coger los libros comunes (dependiendo de alguna condición) de las dos listas y con Join ver cuáles hay en común
        public IEnumerable<Book> JoinExpression()
        {
            var booksAfter2005 = booksCollection.Where(book => book.PublishedDate.Year> 2005);
            var books500Pages = booksCollection.Where(book => book.PageCount > 500);

            return booksAfter2005.Join(books500Pages, x => x.Title, y => y.Title, (x, y) => x);
        }
    }
}