import React from 'react';
import { Link } from 'react-router-dom';
import ProtectedLink from './ProtectedLink';

const Navbar: React.FC = () => {
  return (
    <nav className="bg-gray-800 p-4">
      <div className="max-w-7xl mx-auto flex justify-between items-center">
        {/* Logo */}
        <div className="text-white text-xl font-bold">
          <Link to="/">Rooms</Link>
        </div>

        {/* Links */}
        <div className="hidden md:flex space-x-6">
          <Link to="/" className="text-white hover:text-gray-400">Home</Link>
          <Link to="/login" className="text-white hover:text-gray-400">Login</Link>
          <ProtectedLink to="/register" allowedRoles={["Teacher", "Admin"]} className="text-white hover:text-gray-400">Register User</ProtectedLink>
          <Link to="/contact" className="text-white hover:text-gray-400">Contact</Link>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
