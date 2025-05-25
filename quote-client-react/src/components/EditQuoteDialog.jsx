import { updateQuote } from '../services/quoteService';
import QuoteForm from './QuoteForm';
function EditQuoteDialog({ quote, onClose, onUpdated }) {
    const handleUpdate = (form) => updateQuote(form);
    return (
        <QuoteForm mode="edit" initialData={quote} onSubmit={handleUpdate} onClose={onClose} onSuccess={onUpdated}/>
    );
}

export default EditQuoteDialog;