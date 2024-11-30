import { useEffect, useState } from "react";
import { getEmployees, deleteEmployee } from "../../api";
import { AgGridReact } from "ag-grid-react";
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { useNavigate } from 'react-router-dom'
import { ToastContainer, toast } from "react-toastify";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Button } from '@mui/material';

export default function EmployeeList() {
    const [rowData, setRowData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const [columnDefs, setColumnDefs] = useState([
        {field: 'id', headerName: 'ID'},
        {field: 'name', headerName: 'Name'},
        {field: 'email', headerName: 'Email'},
        {field: 'phoneNumber', headerName: 'Phone Number'},
        {field: 'gender', headerName: 'Gender',
            valueFormatter: (params) => (params.value === 1 ? 
                'Male' : params.value === 2 ? 'Female' : 'Unknown'),
        },
        {field: 'daysWorked', headerName: 'Days Worked in the café'},
        {field: 'cafeName', headerName: 'Café Name'},
        {
            headerName: 'Actions',
            field: 'actions',
            cellRenderer: (params) => (
                <div>
                <button
                    onClick={() => handleEdit(params.data)}
                    style={{ marginRight: '5px', padding: '5px 10px', cursor: 'pointer' }}
                >
                    Edit
                </button>
                <button
                    onClick={() => handleOpenDialog(params.data)}
                    style={{ padding: '5px 10px', cursor: 'pointer', backgroundColor: 'red', color: 'white' }}
                >
                    Delete
                </button>
                </div>
            ),
            sortable: false,
            filter: false,
        },
    ]);
    const [isDialogOpen, setIsDialogOpen] = useState(false);
    const [itemToDelete, setItemToDelete] = useState(null);

    const handleOpenDialog = (id) => {
        setItemToDelete(id);
        setIsDialogOpen(true);
    };

    const handleEdit = (rowData) => {
        navigate(`/employees/edit/${rowData.id}`);
    };

    const handleDelete = async () => {
        setIsDialogOpen(false);
        try {
            console.log(itemToDelete);
            const response = await deleteEmployee(itemToDelete.id) 
            const { success, data, errors } = response.data;
            if (success) {
                toast.success("Employee deleted successfully");
                fetchEmployees();
            }
        } catch (error) {
            toast.error("Faild to delete employee");
        }
    };

    const handleCloseDialog = () => {
        setIsDialogOpen(false);
    };

    const handleAddClick = () => {
        navigate('/employees/add');
    }

    useEffect(() => {
        fetchEmployees();
    }, []);

    const fetchEmployees = async () => {
        try {
            const response = await getEmployees();
            const { success, data, errors } = response.data;

            if (success) {
                setRowData(data);
            } else {
                setError(errors.length > 0 ? errors.join(', ') : "Unknown error occurred");
            } 
        } catch (err) {
            setError(err.message || 'Failed to fetch data');
        } finally {
            setLoading(false);
        }
    }

    if (loading) {
        return <p>Loading...</p>
    }

    if (error) {
        return <p>Error: {error}</p>
    }

    return (
        <>
            <h2>Employee List</h2>
            <div>
                <Button variant="contained" onClick={handleAddClick}>Add New Employee</Button>
            </div>
            <div style={{ width: '100%', height: '400px' }} className="ag-theme-alpine">
                <AgGridReact
                    rowData={rowData} 
                    columnDefs={columnDefs} 
                    defaultColDef={{
                    sortable: true,
                    filter: true,
                    resizable: true,
                    }}
                    pagination={true} 
                    paginationPageSize={10} 
                />
            </div>
            <Dialog open={isDialogOpen} onClose={handleCloseDialog}>
                <DialogTitle>Confirm Deletion</DialogTitle>
                <DialogContent>
                <DialogContentText>
                    Are you sure you want to delete this item? This action cannot be undone.
                </DialogContentText>
                </DialogContent>
                <DialogActions>
                <Button onClick={handleCloseDialog}>Cancel</Button>
                <Button onClick={handleDelete} color="error">
                    Delete
                </Button>
                </DialogActions>
            </Dialog>
            <ToastContainer />
        </>
    );
}