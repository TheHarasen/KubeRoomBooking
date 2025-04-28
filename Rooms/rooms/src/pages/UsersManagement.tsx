import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import { User } from "../types/User";

const UsersManagement = () => {
  const { user } = useAuth(); 

  const [users, setUsers] = useState<User[]>();

  return (
    
  );
}

export default UsersManagement;