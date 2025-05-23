function QuotesTable({ quotes, onDelete, onEdit }) {
    if (quotes.length === 0) return <p> No quotes found.</p>
    const policyTypeMap = {
        0: "Life",
        1: "Auto",
        2: "Home"
    };

    const quoteStatusMap = {
        0: "Pending",
        1: "Approved",
        2: "Rejected"
    };

    const handleDelete = async (id) => {
        //if (!window.confirm("Are you sure you want to delete this quote?")) return;
        try {
            const response = await fetch(`https://localhost:7152/api/Quote/${id}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                onDelete();
            }
            else {
                const error = await response.text();
                console.error('failed to deelte:', error);
                alert('Failed to delete quote');
            }
        }
        catch (err) {
            console.error('error deleting:', err);
            alert('Error deleting quote');
        }
    };


    return (
        <table border="1" cellPadding="8">
            <thead>
                <tr>
                    <th>Client Name</th>
                    <th>Email</th>
                    <th>Age</th>
                    <th>Policy Type</th>
                    <th>Coverage Duration Months</th>
                    <th>Total Premium</th>
                    <th>Status</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {quotes.map((q, index) => (
                    <tr key={index}>
                        <td>{q.clientName}</td>
                        <td>{q.email}</td>
                        <td>{q.clientAge}</td>
                        <td>{policyTypeMap[q.policyType]}</td>
                        <td>{q.coverageDurationMonths}</td>
                        <td>{q.totalPremium}</td>
                        <td>{quoteStatusMap[q.status]}</td>
                        <td>
                            <button onClick={() => handleDelete(q.id)}>Delete</button>
                            <button onClick={() => onEdit(q)}>Edit</button>
                        </td>


                    </tr>
                ))}
            </tbody>
        </table>
    );


}

export default QuotesTable;