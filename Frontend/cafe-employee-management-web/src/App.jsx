import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import CafeList from "./components/cafe/cafeList";
import EmployeeList from "./components/employee/EmployeeList";
import EmployeeForm from "./components/employee/employeeForm";
import CafeForm from "./components/cafe/CafeForm";

function App() {
  return (
    <BrowserRouter>
      <nav>
        <ul>
          <li><Link to="/cafes">Cafes</Link></li>
          <li><Link to="/employees">Employee</Link></li>
        </ul>
      </nav>
      <Routes>
        <Route path="/cafes" element={<CafeList />} />
        <Route path="/employees" element={<EmployeeList />} />
        <Route path="/employees/add" element={<EmployeeForm />} />
        <Route path="/employees/edit/:id" element={<EmployeeForm />} />
        <Route path="/cafes/add" element={<CafeForm />} />
        <Route path="/cafes/edit/:id" element={<CafeForm />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
