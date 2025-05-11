import { ReactNode } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from "../contexts/AuthContext";

interface ProtectedLinkProps {
  to: string;
  className: string;
  children: ReactNode;
  allowedRoles?: string[]; 
}

const ProtectedLink = ({ to, children, allowedRoles, className = "" }: ProtectedLinkProps) => {
  const { loggedIn } = useAuth();

  if (!loggedIn || (allowedRoles && !allowedRoles.includes(loggedIn.role)))
    return <></>
  else
    return (
      <Link to={to} className={className}>
        {children}
      </Link>
    );
}

export default ProtectedLink;