import NavBar from './components/Navbar'
import { Route, Routes } from 'react-router-dom'
import Home from './pages/Home';
import Contact from './pages/Contact';
import Footer from './components/Footer';
import Login from './pages/Login';
import { AuthProvider } from './contexts/AuthContext';
import Register from './pages/Register';
import ProtectedRoute from './components/ProtectedRoute';

function App() {
  return (
    <AuthProvider>
      <NavBar />
        <main>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={
                <ProtectedRoute allowedRoles={["Admin", "Teacher"]}>
                  <Register />
              </ProtectedRoute>}
            />
          </Routes>
        </main>
      <Footer />
    </AuthProvider>
  );
}

export default App
