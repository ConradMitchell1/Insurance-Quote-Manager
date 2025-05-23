const BASE_URL = "https://insurance-quote-api.azurewebsites.net/api/Quote";

export async function getQuotes(searchQuotes) {
    const url = new URL(BASE_URL);
    if (searchQuotes) {
        url.searchParams.append('searchTerm', searchQuotes);
    }
    const response = await fetch(url);

    if (!response.ok) {
        throw new Error(`Server error: ${response.status}`);
    }

    const text = await response.text();
    return text ? JSON.parse(text) : [];
}

export async function createQuote(quote) {
    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(quote)
    });
    return response;
}

export async function updateQuote(quote) {
    const response = await fetch(BASE_URL, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(quote)
    });
    return response;
}

export async function deleteQuote(id) {
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
    });
    return response;
}