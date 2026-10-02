import { NavLink } from "react-router-dom";
import LogoutButton from "./LogoutButton";

interface NavbarProps {
  linkLabels: string[];
  linkTo: string[];

  withLogoutButton?: boolean;
}

export default function Navbar({
  linkLabels,
  linkTo,
  withLogoutButton = false,
}: NavbarProps) {
  const getLinkStyle = ({ isActive }: { isActive: boolean }): string => {
    const baseStyle = "text-sm font-medium transition-colors duration-200";
    const activeStyle = "text-blue-600 border-b-2 border-blue-600 pb-1";
    const inactiveStyle =
      "text-gray-500 hover:text-gray-900 pb-1 border-b-2 border-transparent";

    return `${baseStyle} ${isActive ? activeStyle : inactiveStyle}`;
  };

  return (
    <header className="w-full bg-white border-b border-gray-200">
      <div className="max-w-7xl mx-auto px-6 py-4">
        <nav className="flex items-center gap-6">
          {linkLabels.map((label, index) => (
            <NavLink key={index} to={linkTo[index]} className={getLinkStyle}>
              {label}
            </NavLink>
          ))}
          {withLogoutButton && (
            <div className="ml-auto">
              <LogoutButton />
            </div>
          )}
        </nav>
      </div>
    </header>
  );
}
