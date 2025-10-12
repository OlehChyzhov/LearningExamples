namespace DesignPatterns.SOLID
{
    // Interface Segregation Principle (ISP) states that interfaces should be split so that clients 
    // don't have to implement methods they do not use.

    #region Default Objects

    public class Document
    {
        private string _name;
        private string _extension;

        public Document(string name, string extension)
        {
            _name = name;
            _extension = extension;
        }

        public override string ToString()
        {
            return $"Name: {_name}, Extension: {_extension}";
        }
    }

    #endregion

    #region Problem

    // Let's imagine that you need a printer that can Print, Scan, and Fax.
    // So you've created the following interface.
    public interface IMultiPrinter
    {
        void Print(Document document);
        void Scan(Document document);
        void Fax(Document document);
    }

    // A multifunction printer works fine with this interface
    public class MultiFunctionalPrinter : IMultiPrinter
    {
        public void Fax(Document document)
        {
            Console.WriteLine($"Fax: {document}");
        }

        public void Print(Document document)
        {
            Console.WriteLine($"Print: {document}");
        }

        public void Scan(Document document)
        {
            Console.WriteLine($"Scan: {document}");
        }
    }

    // But an old printer can only print.
    // Because the interface is too large, we are forced to implement methods we don't need.
    public class OldPrinter : IMultiPrinter
    {
        public void Print(Document document)
        {
            Console.WriteLine($"Print: {document}");
        }

        public void Fax(Document document)
        {
            throw new NotImplementedException(); // Not needed
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException(); // Not needed
        }
    }

    // That is the problem. If you divided your interface, it could be avoided.

    #endregion

    #region Solution

    // Split large interface into smaller, more specific interfaces
    public interface IPrintable
    {
        void Print(Document document);
    }

    public interface IScannable
    {
        void Scan(Document document);
    }

    public interface IFaxable
    {
        void Fax(Document document);
    }

    // OldPrinter now only implements what it needs
    public class SimplePrinter : IPrintable
    {
        public void Print(Document document)
        {
            Console.WriteLine($"Print: {document}");
        }
    }

    // This printer supports all functions using multiple interfaces
    public class ModernPrinter : IPrintable, IScannable, IFaxable
    {
        public void Print(Document document)
        {
            Console.WriteLine($"Print: {document}");
        }

        public void Scan(Document document)
        {
            Console.WriteLine($"Scan: {document}");
        }

        public void Fax(Document document)
        {
            Console.WriteLine($"Fax: {document}");
        }
    }

    // If you ever need a more complex interface, you can use interface inheritance
    public interface IMultiFunctionalDevice : IPrintable, IScannable, IFaxable { }

    #endregion
}