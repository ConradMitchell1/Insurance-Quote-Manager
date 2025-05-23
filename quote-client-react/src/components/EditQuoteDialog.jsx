import { useState } from 'react';
import { updateQuote } from '../services/quoteService';
function EditQuoteDialog({ quote, onClose, onUpdated }) {
    const [form, setForm] = useState({ ...quote });
    const [errors, setErrors] = useState([]);

    const handleChange = (field, value) => {
        setForm(prev => ({ ...prev, [field]: value }));
    };

    const handleUpdate = async () => {
        try {
            const response = await updateQuote(form);
            if (response.ok) {
                alert('Quote Updated');
                onClose();
                onUpdated();
            }
            else {
                const errorJson = await response.json();
                const validationErrors = [];
                if (errorJson.errors) {
                    for (const key in errorJson.errors) {
                        validationErrors.push(...errorJson.errors[key]);
                    }
                }
                else {
                    validationErrors.push(errorJson.title || 'Unknown error');
                }
                setErrors(validationErrors);
            }
        }
        catch (error) {
            setErrors([error.message || 'Network error']);
        }
    };
    return (
        <div className="dialog-overlay">
            <div className="dialog">
                <h2>Edit Quote</h2>
                {errors.length > 0 && (
                    <ul style={{ color: 'red' }}>
                        {errors.map((e, i) => <li key={i}>{e}</li>)}
                    </ul>
                )}
                <input placeholder="Client Name" value={form.clientName} onChange={(e) => handleChange('clientName', e.target.value)} />
                <input placeholder="Email" value={form.email} onChange={(e) => handleChange('email', e.target.value)} />
                <input placeholder="Age" type="number" value={form.clientAge} onChange={(e) => handleChange('clientAge', parseInt(e.target.value))} />
                <select value={form.policyType} onChange={(e) => handleChange('policyType', parseInt(e.target.value))}>
                    <option value={0}>Life</option>
                    <option value={1}>Auto</option>
                    <option value={2}>Home</option>
                </select>
                <select value={form.quoteStatus} onChange={(e) => handleChange('quoteStatus', parseInt(e.target.value))}>
                    <option value={0}>Pending</option>
                    <option value={1}>Approved</option>
                    <option value={2}>Rejected</option>
                </select>
                {form.policyType === 2 && (
                    <>
                        <input placeholder="Property Type" value={form.propertyType} onChange={(e) => handleChange('propertyType', e.target.value)} />
                        <input placeholder="Construction Type" value={form.constructionType} onChange={(e) => handleChange('constructionType', e.target.value)} />
                        <input placeholder="Property Age" type="number" value={form.propertyAge} onChange={(e) => handleChange('propertyAge', e.target.value)} />
                    </>
                )}
                <button onClick={handleUpdate}>Update</button>
                <button onClick={onClose}>Cancel</button>
            </div>
        </div>
    );
}

export default EditQuoteDialog;