export default function AboutPage() {
  return (
    <div className="w-full max-w-2xl mx-auto bg-white rounded-xl border border-gray-200 shadow-sm p-8">
      {/* Category Pill */}
      <span className="inline-block px-3 py-1 text-xs font-semibold text-blue-700 bg-blue-50 rounded-full mb-4">
        About Us
      </span>

      {/* Main Heading */}
      <h1 className="text-2xl font-bold text-gray-900 mb-3">
        AliMA Corporation
      </h1>
      <h2 className="text-lg font-semibold text-gray-800 mb-4">
        Job Application Management Dashboard
      </h2>
      {/* Short Description Paragraph */}
      <p className="text-gray-600 leading-relaxed text-sm">
        This internal dashboard app is designed to help AliMA Corporation
        employees manage their job applications efficiently. It provides a
        centralized platform to track and update job applications, ensuring that
        the company is maintaining a healthy communication with shortlisted
        candidates.
      </p>
    </div>
  );
}
