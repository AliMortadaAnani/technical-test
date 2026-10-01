export default function AboutPage() {
  return (
    <div className="w-full max-w-2xl mx-auto bg-white rounded-xl border border-gray-200 shadow-sm p-8">
      {/* Category Pill */}
      <span className="inline-block px-3 py-1 text-xs font-semibold text-blue-700 bg-blue-50 rounded-full mb-4">
        About Us
      </span>

      {/* Main Heading */}
      <h1 className="text-2xl font-bold text-gray-900 mb-3">
        Job Application Portal
      </h1>

      {/* Short Description Paragraph */}
      <p className="text-gray-600 leading-relaxed text-sm">
        Our recruitment platform simplifies the hiring process by helping teams
        track applicants seamlessly from submission to final decision. We
        provide a clean, reliable, and transparent workflow to make candidate
        management faster and more efficient.
      </p>
    </div>
  );
}
