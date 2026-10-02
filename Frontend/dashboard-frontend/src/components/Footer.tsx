export default function Footer() {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="w-auto  bg-white border-t border-gray-200 mt-1">
      <div className="max-w-7xl mx-auto px-6 py-6 flex flex-col sm:flex-row items-center justify-between gap-4 text-sm text-gray-500">
        <p>
          &copy; {currentYear} AliMA Corporation <br /> All rights reserved
        </p>
        <p>Designed and built by Ali Mortada Anani</p>
      </div>
    </footer>
  );
}
