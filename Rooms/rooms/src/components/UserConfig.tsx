import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import { RegisterRequest } from "../services/UserService";
import { User } from "../types/User";

interface UserProps{
  func: (request: RegisterRequest) => Promise<{ message: string, success: boolean }>;
  passwordHtml: string;
  passwordTitle: string;
  user?: User;
}

const UserConfig = ({ func, passwordHtml, passwordTitle, user}: UserProps) => {

  const { loggedIn } = useAuth(); 
  const [ email, setMail] = useState<string>(user?.email || "");
  const [ firstName, setFirstname] = useState<string>(user?.firstName || "");
  const [ lastName, setLastname] = useState<string>(user?.lastName || "");
  const [ password, setPassword] = useState<string>("");
  const [ passwordRepeat, setPasswordRepeat] = useState<string>("");
  const [ role, setRole ] = useState<string>(user?.role || "student");

  const handle = async () => {
    const result = await func({  email, firstName, lastName, password, passwordRepeat, role });
    console.log(result);
  }
  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-white rounded-2xl shadow-md space-y-6">
      <h2 className="text-2xl font-semibold text-center">Create an Account</h2>

      <div className="space-y-4">
        <div>
          <label htmlFor="email" className="block text-sm font-medium text-gray-700">Email</label>
          <input
            type="email"
            name="email"
            id="email"
            value={email}
            onChange={(e) => setMail(e.target.value)}
            className="mt-1 w-full px-3 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div>
          <label htmlFor="firstname" className="block text-sm font-medium text-gray-700">First Name</label>
          <input
            type="text"
            name="firstname"
            id="firstname"
            value={firstName}
            onChange={(e) => setFirstname(e.target.value)}
            className="mt-1 w-full px-3 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div>
          <label htmlFor="lastname" className="block text-sm font-medium text-gray-700">Last Name</label>
          <input
            type="text"
            name="lastname"
            id="lastname"
            value={lastName}
            onChange={(e) => setLastname(e.target.value)}
            className="mt-1 w-full px-3 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div>
          <label htmlFor="password" className="block text-sm font-medium text-gray-700">Password</label>
          <input
            type="password"
            name="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="mt-1 w-full px-3 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div>
          <label htmlFor={passwordHtml} className="block text-sm font-medium text-gray-700">{passwordTitle}</label>
          <input
            type="password"
            name={passwordHtml}
            id={passwordHtml}
            value={passwordRepeat}
            onChange={(e) => setPasswordRepeat(e.target.value)}
            className="mt-1 w-full px-3 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div>
          <label htmlFor="role" className="block text-sm font-medium text-gray-700">Role</label>
          <select
            id="role"
            name="role"
            value={role}
            onChange={(e) => setRole(e.target.value)}
            className="mt-1 w-full px-3 py-2 border border-gray-300 bg-white rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="student">Student</option>
            { loggedIn != null && loggedIn.role == "Admin" && <option value="teacher">Teacher</option>}
          </select>
        </div>


        <button
          onClick={handle}
          className="w-full py-2 px-4 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 transition">
          Register
        </button>
      </div>
    </div>
  );
}

export default UserConfig;