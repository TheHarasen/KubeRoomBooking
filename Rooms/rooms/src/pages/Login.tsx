import { useState } from 'react';
import { useAuth } from '../contexts/AuthContext';

const Login = () => {
  const { loggedIn, logout, login } = useAuth();
  const [ email, setMail ] = useState<string>("");
  const [ pass, setPass ] = useState<string>("");
  const [ error, setError ] = useState<string>("");

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const result = await login(email, pass);
    // get result and use
    console.log(result);
    if (result == null) {
      setError("Fail")
      return;
    }
    setMail("");
    setPass("");
  };

  return (<div className="flex flex-col items-center justify-center min-h-screen bg-gray-50 p-4">
    <div className="bg-white shadow-lg rounded-lg p-6 w-full sm:w-96">
      <h1 className="text-2xl font-semibold text-center mb-4">Welcome {loggedIn == null ? "" : loggedIn.email + " " + loggedIn.firstName}</h1>
      {loggedIn !== null && 
        <button
          onClick={logout}
          className="w-full py-2 px-4 mb-6 bg-red-500 text-white rounded-md hover:bg-red-600 focus:outline-none focus:ring-2 focus:ring-red-500"
        >
          Logout
        </button>
      }

      <form className="space-y-4" onSubmit={handleLogin}>
        <div>
          <label htmlFor="email" className="block text-sm font-medium text-gray-700">
            Email
          </label>
          <input
            id="email"
            name="email"
            type="email"
            value={email}
            onChange={(e) => setMail(e.target.value)}
            required
            autoComplete="username"
            className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500"
          />
        </div>

        <div>
          <label htmlFor="password" className="block text-sm font-medium text-gray-700">
            Password
          </label>
          <input
            id="password"
            name="password"
            type="password"
            value={pass}
            onChange={(e) => setPass(e.target.value)}
            required
            autoComplete="current-password"
            className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500"
          />
        </div>

        <button
          type='submit'
          className="w-full py-2 px-4 bg-indigo-600 text-white rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500"
        >
          Login
        </button>
      </form>
      {error && <div className="mt-4 text-red-600 text-sm text-center">{error}</div>}
    </div>
  </div>
);

}

export default Login;
