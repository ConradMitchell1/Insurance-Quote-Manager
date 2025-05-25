import { createQuote } from '../services/quoteService';
import QuoteForm from './QuoteForm';

function CreateQuote({ onCreated, onClose }) {
    const handleCreate = (form) => createQuote(form);
    return (
        <QuoteForm mode="create" onSubmit={handleCreate} onClose={onClose} onSuccess={onCreated}/>
    );
}

export default CreateQuote;