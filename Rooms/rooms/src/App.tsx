import NavBar from './components/Navbar'
import { Route, Routes } from 'react-router-dom'
import Home from './pages/Home';
import Contact from './pages/Contact';
import Footer from './components/Footer';
import Login from './pages/Login';
import { AuthProvider } from './contexts/AuthContext';

function App() {
  return (
    <AuthProvider>
      <NavBar />
      <main>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </main>
      <Footer />
    </AuthProvider>
  );
}

export default App
