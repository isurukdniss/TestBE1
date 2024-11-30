import { useEffect, useState } from "react";
import { getCafes } from "../../api";
import { AgGridReact } from "ag-grid-react";
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom'

export default function CafeList() {

    const navigate = useNavigate();
    const [rowData, setRowData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [columnDefs, setColumnDefs] = useState([
        { field: 'logo', headerName: 'Logo' },
        { field: 'name', headerName: 'Name' },
        { field: 'description', headerName: 'Description' },
        { field: 'employees', headerName: 'Employees' },
        { field: 'location', headerName: 'Location' },
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
                        onClick={() => handleDelete(params.data)}
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

    const handleAddClick = () => {
        navigate('/cafes/add');
    }

    const handleEdit = (rowData) => {
        navigate(`/cafes/edit/${rowData.id}`);
    };

    const handleDelete = (rowData) => {
        alert(`Deleting row: ${JSON.stringify(rowData)}`);
        // Add logic to handle delete the row
    };

    useEffect(() => {
        loadCafes();
    }, []);

    const loadCafes = async () => {
        try {
            const response = await getCafes();
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
            <h2>Cafe List</h2>
            <div>
                <Button variant="contained" onClick={handleAddClick}>Add Cafe</Button>
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
        </>
    );
}